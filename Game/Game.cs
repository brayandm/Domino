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
    private ITeamOrder _teamOrder;
    private ITokenVisibility _tokenVisibility;
    private IJoinable _joinable;
    private IIdJoinable _idJoinable;
    private IOrderPlayerSequence _orderPlayerSequence;
    private IRoundScoreTeam _roundScoreTeam;
    private IRoundScorePlayer _roundScorePlayer;
    private IRoundWinnerRule _roundWinnerRule;
    private IScoreTeam _scoreTeam;
    private IWinnerRule _winnerRule;

    public void NewRoundGame()
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

    public Game()
    {
        this._boxGenerator = (IBoxGenerator)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(IBoxGenerator));
        this._teamGenerator = (ITeamGenerator)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(ITeamGenerator));
        this._teamOrder = (ITeamOrder)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(ITeamOrder));
        this._tokenVisibility = (ITokenVisibility)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(ITokenVisibility));
        this._joinable = (IJoinable)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(IJoinable));
        this._idJoinable = (IIdJoinable)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(IIdJoinable));
        this._orderPlayerSequence = (IOrderPlayerSequence)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(IOrderPlayerSequence));
        this._roundScoreTeam = (IRoundScoreTeam)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(IRoundScoreTeam));
        this._roundScorePlayer = (IRoundScorePlayer)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(IRoundScorePlayer));
        this._roundWinnerRule = (IRoundWinnerRule)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(IRoundWinnerRule));
        this._scoreTeam = (IScoreTeam)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(IScoreTeam));
        this._winnerRule = (IWinnerRule)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(IWinnerRule));
        
        List<Team> teams = this._teamGenerator.GetTeams();

        Debug.Assert(teams.Count > 0);

        List<Player> players = this._teamOrder.GetTeamOrder(teams);
        
        for(int i = 0 ; i < players.Count ; i++)
        {
            this._boards.Add(new Board());
        }

        this._teamInfo = new TeamInfo(teams, players, _boards, this._orderPlayerSequence);   
    }

    public Game(List<Team> teams) : this()
    {
        Debug.Assert(teams.Count > 0);
        
        List<Player> players = this._teamOrder.GetTeamOrder(teams);

        this._boards = new List<Board>();
        
        for(int i = 0 ; i < players.Count ; i++)
        {
            this._boards.Add(new Board());
        }

        this._teamInfo = new TeamInfo(teams, players, _boards, this._orderPlayerSequence);   
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

        int index = player.Strategy.ChooseTokenIndex(tokens, player, this._teamInfo.Teams, this.GetPlayersAndBoardTokensVisibleByPlayer(player), this._table.GetTokensWithoutProtection());

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

    public void WatchThesePlayers(ProtectedToken token, List<Player> players)
    {
        foreach(Player player in players)
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

        try
        {
            tokenDealer.Distribute(this._box, this._boards);
        }
        catch (TokenUnavailabilityException)
        {
            throw new TokenDealerUnavailabilityException();
        }

        foreach(Player player in this._teamInfo.Players)
        {
            foreach(ProtectedToken token in this.GetPlayerBoard(player).GetTokens())
            {
                this.WatchThesePlayers(token, this._tokenVisibility.GetTokenVisibilityPlayers(player, this._teamInfo.Teams));
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

    public int GetRoundPlayerScore(Player player)
    {
        return this._roundScorePlayer.GetScore(this, player);
    }

    public int GetRoundTeamScore(Team team)
    {
        return this._roundScoreTeam.GetScore(this, team);
    }

    public List<Team> GetRoundWinners()
    {
        return this._roundWinnerRule.GetWinners(this);
    }

    public void SaveRoundGameResults()
    {
        foreach(Team team in this.GetAllTeams())
        {
            this._history.GetCurrentHistoryRound().SetTeamScore(team, this.GetRoundTeamScore(team));
        }

        this._history.GetCurrentHistoryRound().SetWinners(this.GetRoundWinners());
    }

    public List<int> GetTeamAllRoundScores(Team team)
    {
        List<int> scores = new List<int>();

        foreach(HistoryRound historyRound in this._history.GetHistoryRounds())
        {
            scores.Add(historyRound.GetTeamScore(team));
        }
        
        return scores;
    }

    public int GetTeamAllRoundScore(Team team)
    {
        return this._scoreTeam.GetScore(this, team);
    }

    public List<Team> GetWinnersAllRound()
    {
        return this._winnerRule.GetWinners(this);
    }

    public int GetNumberOfRounds()
    {
        return this._history.GetNumberOfRounds();
    }

    public void EndRoundGame()
    {
        this._history.GetCurrentHistoryRound().EndRound();
        this.SaveRoundGameResults();
    }

    public bool IsCurrentRoundEnded()
    {
        return this._history.GetCurrentHistoryRound().IsRoundEnded();
    }

    public List<Token> GetBoardTokens(Board board)
    {
        List<ProtectedToken> protectedTokens = board.GetTokens();

        List<Token> tokens = new List<Token>();

        foreach(ProtectedToken protectedToken in protectedTokens)
        {
            tokens.Add(protectedToken.GetTokenWithoutVisibility());
        }

        return tokens;
    }

    public List<Token> GetTabletokens()
    {
        return this._table.GetTokensWithoutProtection();
    }

    public ProtectedToken? GetTokenPositionMiddle()
    {
        List<Move> moves = this._history.GetCurrentHistoryRound().GetMoves();

        foreach(Move move in moves)
        {
            if(move.Position == Position.Middle)
            {
                return move.Token;
            }
        }

        return null;
    }

    public int GetPositionMiddle()
    {   
        ProtectedToken? protectedToken = this.GetTokenPositionMiddle();

        if(protectedToken is ProtectedToken)
        {
            List<ProtectedToken> protectedTokens = this._table.GetTokens();

            for(int i = 0 ; i < protectedTokens.Count ; i++)
            {
                if(protectedToken == protectedTokens[i])
                {
                    return i;
                }
            }
        }
        
        return -1;
    }

    public void DrawTokenPlayer(Player player)
    {
        try
        {
            ProtectedToken protectedToken = this._box.Take();

            this.WatchThesePlayers(protectedToken, this._tokenVisibility.GetTokenVisibilityPlayers(player, this._teamInfo.Teams));
            protectedToken.NewOwner(player);

            this.GetPlayerBoard(player).Add(protectedToken);

            this._history.GetCurrentHistoryRound().PlayMove(new Move(player, null, Position.Draw));
        }
        catch (TokenUnavailabilityException)
        {
            
        }
    }

    public void DrawTokenCurrentPlayer()
    {
        this.DrawTokenPlayer(this.GetCurrentPlayer());
    }

    public bool LastMoveWasDraw()
    {
        return this._history.GetCurrentHistoryRound().LastMoveWasDraw();
    }

    public Dictionary<Player, List<Token?>> GetPlayersAndBoardTokensVisibleByPlayer(Player player)
    {
        Dictionary<Player, List<Token?>> playerBoard = new Dictionary<Player, List<Token?>>();

        foreach(Player otherPlayer in this._teamInfo.Players)
        {
            playerBoard.Add(otherPlayer, new List<Token?>());

            Board board = this.GetPlayerBoard(otherPlayer);

            foreach(ProtectedToken token in board.GetTokens())
            {
                if(token.IsVisible(player))
                {
                    playerBoard[otherPlayer].Add(token.GetTokenWithoutVisibility());
                }
                else
                {
                    playerBoard[otherPlayer].Add(null);
                }
            }
        }

        return playerBoard;
    }

    public void SetGameToEnded()
    {
        this._history.SetGameToEnded();
    }

    public bool IsGameEnded()
    {
        return this._history.IsGameEnded();
    }
}