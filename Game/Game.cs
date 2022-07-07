using System.Diagnostics;

class Game
{
    private TeamInfo _teamInfo;

    private List<Board> _boards = new List<Board>();
    private Box _box = new Box();
    private Table _table = new Table();

    private History _history = new History();
    
    private IBoxGenerator _boxGenerator;
    private ITeamGenerator _teamGenerator;
    private IJoinable _joinable;
    private IIdJoinable _idJoinable;
    private IOrderPlayerSequence _orderPlayerSequence;

    public void NewGame()
    {
        this._teamInfo.OrderPlayer.RestartOrder();

        foreach(Board board in this._boards)
        {
            board.Clear();
        }

        this._box.Init(this._boxGenerator);
        this._table.Clear();
        this._history.NewHistoryRound();
    }

    public Game(IBoxGenerator boxGenerator, ITeamGenerator teamGenerator, IJoinable joinable, IIdJoinable idJoinable, IOrderPlayerSequence orderPlayerSequence)
    {
        this._boxGenerator = boxGenerator;
        this._teamGenerator = teamGenerator;
        this._joinable = joinable;
        this._idJoinable = idJoinable;
        this._orderPlayerSequence = orderPlayerSequence;
        
        Tuple<List<Team>, List<Player>> teams = this._teamGenerator.GetTeams();
        
        for(int i = 0 ; i < teams.Item2.Count ; i++)
        {
            this._boards.Add(new Board());
        }

        this._teamInfo = new TeamInfo(teams.Item1, teams.Item2, _boards, this._orderPlayerSequence);
        
        this.NewGame();
    }

    public List<ProtectedToken> GetBoardTokensVisibleForPlayer(Player player, Board board)
    {
        List<ProtectedToken> tokens = board.GetTokens();

        List<ProtectedToken> visibleTokens = new List<ProtectedToken>();

        foreach(ProtectedToken token in tokens)
        {
            if(token.IsVisible(player))
            {
                visibleTokens.Add(token);
            }
        }

        return visibleTokens;
    }

    public List<ProtectedToken> GetPlayerTokensPlayable(Player player, IJoinable joinable)
    {
        Board board = this._teamInfo.PlayerBoard[player];
        
        List<ProtectedToken> tokens = new List<ProtectedToken>();

        foreach(ProtectedToken token in GetBoardTokensVisibleForPlayer(player, board))
        {
            if(joinable.IsJoinable(this, token, this._table.LeftToken))
            {
                tokens.Add(token);
                break;
            }
            if(joinable.IsJoinable(this, this._table.RightToken, token))
            {
                tokens.Add(token);
                break;
            }

            token.Rotate();

            if(joinable.IsJoinable(this, token, this._table.LeftToken))
            {
                tokens.Add(token);
                break;
            }
            if(joinable.IsJoinable(this, this._table.RightToken, token))
            {
                tokens.Add(token);
                break;
            }
        }

        return tokens;
    }

    public List<ProtectedToken> GetCurrentPlayerTokensPlayable(IJoinable joinable)
    {
        return this.GetPlayerTokensPlayable(this._teamInfo.OrderPlayer.CurrentPlayer(), joinable);
    }

    public Tuple<ProtectedToken?, Position> SelectCurrentPlayerMove(IJoinable joinable)
    {
        List<ProtectedToken> protectedTokens = this.GetCurrentPlayerTokensPlayable(joinable);

        if(protectedTokens.Count == 0)
        {
            return new Tuple<ProtectedToken?, Position>(null, Position.Pass);
        }

        Player player = this._teamInfo.OrderPlayer.CurrentPlayer();

        List<Token> tokens = new List<Token>();

        foreach(ProtectedToken protectedToken in protectedTokens)
        {
            Debug.Assert(protectedToken.IsVisible(player));

            tokens.Add(protectedToken.GetTokenWithoutVisibility());
        }

        Debug.Assert(tokens.Count > 0);

        int index = player.Strategy.ChooseTokenIndex(tokens, this._table.GetTokens());

        ProtectedToken tokenToPlay = protectedTokens[index];

        if(!this._table.Empty && joinable.IsJoinable(this, tokenToPlay, this._table.LeftToken))
        {
            return new Tuple<ProtectedToken?, Position>(tokenToPlay, Position.Left);
        }

        if(!this._table.Empty && joinable.IsJoinable(this, this._table.RightToken, tokenToPlay))
        {
            return new Tuple<ProtectedToken?, Position>(tokenToPlay, Position.Right);
        }

        tokenToPlay.Rotate();

        if(!this._table.Empty && joinable.IsJoinable(this, tokenToPlay, this._table.LeftToken))
        {
            return new Tuple<ProtectedToken?, Position>(tokenToPlay, Position.Left);
        }

        if(!this._table.Empty && joinable.IsJoinable(this, this._table.RightToken, tokenToPlay))
        {
            return new Tuple<ProtectedToken?, Position>(tokenToPlay, Position.Right);
        }
        
        return new Tuple<ProtectedToken?, Position>(tokenToPlay, Position.Middle);
    }

    public void WatchAllPlayers(ProtectedToken token)
    {
        foreach(Player player in this._teamInfo.Players)
        {
            token.Watch(player);
        }
    }

    public void PlayToken(ProtectedToken token, Position position)
    {
        this.WatchAllPlayers(token);
        
        foreach(Board board in _boards)
        {
            if(board.Contains(token))
            {
                if(position == Position.Left)
                {
                    if(_table.LeftToken is ProtectedToken)
                    {
                        if(!this._joinable.IsJoinable(this, token, this._table.LeftToken))
                        {
                            token.Rotate();
                        }

                        Debug.Assert(this._joinable.IsJoinable(this, token, this._table.LeftToken));

                        this._table.Put(token, false);
                    }
                }

                if(position == Position.Middle)
                {
                    this._table.Put(token, false);
                }

                if(position == Position.Right)
                {
                    if(_table.RightToken is ProtectedToken)
                    {
                        if(!this._joinable.IsJoinable(this, this._table.RightToken, token))
                        {
                            token.Rotate();
                        }

                        Debug.Assert(this._joinable.IsJoinable(this, this._table.RightToken, token));

                        this._table.Put(token, true);
                    }
                }

                board.Remove(token);
            }
        }
    }

    public void ProcessCurrentTurn()
    {
        Tuple<ProtectedToken?, Position> playerMove = this.SelectCurrentPlayerMove(this._joinable);

        if(playerMove.Item2 != Position.Pass)
        {
            Debug.Assert(playerMove.Item1 is ProtectedToken);
            
            if(playerMove.Item1 is ProtectedToken)
            {
                this.PlayToken((ProtectedToken)playerMove.Item1, playerMove.Item2);
            }
        }

        Move move = new Move(this._teamInfo.OrderPlayer.CurrentPlayer(), playerMove.Item1, playerMove.Item2);

        this._history.GetCurrentHistoryRound().PlayMove(move);
    }

    public void DistributeTokens(ITokenDealer tokenDealer)
    {
        this._history.GetCurrentHistoryRound().Distributed();

        tokenDealer.Distribute(this._box, this._boards);

        foreach(Player player in this._teamInfo.Players)
        {
            foreach(ProtectedToken token in this.GetPlayerBoard(player).GetTokens())
            {
                token.Watch(player);
                token.NewOwner(player);
            }
        }
    }

    public int GetNumberOfContiguousPassedTurns()
    {
        return this._history.GetCurrentHistoryRound().GetContiguousPassedTurns();
    }

    public int GetNumberOfPlayers()
    {
        return this._teamInfo.Players.Count;
    }

    public void NextPlayer()
    {
        this._teamInfo.OrderPlayer.NextPlayer();
    }

    public bool PlayerPlayedAllTokens(Player player)
    {
        return this._teamInfo.PlayerBoard[player].Count == 0;
    }

    public bool CurrentPlayerPlayedAllTokens()
    {
        return this.PlayerPlayedAllTokens(this._teamInfo.OrderPlayer.CurrentPlayer());
    }

    public int GetNumberOfMoves()
    {
        return this._history.GetCurrentHistoryRound().GetNumberOfMoves();
    }

    public string GetTableString()
    {
        return this._table.ToString();
    }

    public string GetBoardString(Board board)
    {
        return board.ToString();
    }

    public Player GetCurrentPlayer()
    {
        return this._teamInfo.OrderPlayer.CurrentPlayer();
    }

    public List<Player> GetAllPlayers()
    {
        return this._teamInfo.Players;
    }

    public Board GetPlayerBoard(Player player)
    {
        return this._teamInfo.PlayerBoard[player];
    }

    public string GetCurrentPlayerBoardString()
    {
        return GetBoardString(this.GetPlayerBoard(this.GetCurrentPlayer()));
    }

    public int GetPlayerTotalPassedTurns(Player player)
    {
        return this._history.GetCurrentHistoryRound().GetPlayerTotalPassedTurns(player);
    }

    public bool IsDistributed()
    {
        return this._history.GetCurrentHistoryRound().IsDistributed();
    }

    public Move? GetLastMove()
    {
        return this._history.GetCurrentHistoryRound().GetLastMove();
    }

    public void ReversePlayerOrder()
    {
        this._teamInfo.OrderPlayer.Reverse();
    }

    public List<Team> GetAllTeams()
    {
        return this._teamInfo.Teams;
    }

    public Team GetPlayerTeam(Player player)
    {
        return this._teamInfo.PlayerTeam[player];
    }

    public bool IsIdJoinable(string idA, string idB)
    {
        return this._idJoinable.IsIdJoinable(idA, idB);
    }
}