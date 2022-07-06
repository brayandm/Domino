using System.Diagnostics;

class Game
{
    private TeamInfo _teamInfo;

    private List<Board> _boards;
    private Box _box;
    private Table _table;

    private History _history;

    public Game(IBoxGenerator boxGenerator, ITeamGenerator teamGenerator)
    {
        Tuple<List<Team>, List<Player>> teams = teamGenerator.GetTeams();
        this._boards = new List<Board>();
        
        for(int i = 0 ; i < teams.Item2.Count ; i++)
        {
            this._boards.Add(new Board());
        }

        this._teamInfo = new TeamInfo(teams.Item1, teams.Item2, _boards);
        this._box = new Box(boxGenerator);
        this._table = new Table();
        this._history = new History();
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

    public bool MatchTokenWithFace(Token token, IFace face)
    {
        if(token.Faces.Item1.Id == face.Id || token.Faces.Item2.Id == face.Id)return true;
        return false;
    }

    public bool MatchTokenWithFaces(Token token, Tuple<IFace, IFace> faces)
    {
        if(this.MatchTokenWithFace(token, faces.Item1) || this.MatchTokenWithFace(token, faces.Item2))return true;
        return false;
    }

    public bool MatchTokenWithLeftFace(Token token)
    {
        Tuple<IFace, IFace>? availableFaces = this._table.AvailableFaces;

        if(availableFaces == null)
        {
            return true;
        }

        return this.MatchTokenWithFace(token, availableFaces.Item1);
    }

    public bool MatchTokenWithRightFace(Token token)
    {
        Tuple<IFace, IFace>? availableFaces = this._table.AvailableFaces;

        if(availableFaces == null)
        {
            return true;
        }

        return this.MatchTokenWithFace(token, availableFaces.Item2);
    }

    public bool MatchTokenWithBothFaces(Token token)
    {
        return this.MatchTokenWithLeftFace(token) || this.MatchTokenWithRightFace(token);
    }

    public List<ProtectedToken> GetPlayerTokensPlayable(Player player)
    {
        Board board = this._teamInfo.PlayerBoard[player];
        
        List<ProtectedToken> tokens = new List<ProtectedToken>();

        foreach(ProtectedToken token in GetBoardTokensVisibleForPlayer(player, board))
        {
            if(this.MatchTokenWithBothFaces(token.GetTokenWithoutVisibility()))
            {
                tokens.Add(token);
            }
        }

        return tokens;
    }

    public List<ProtectedToken> GetCurrentPlayerTokensPlayable()
    {
        return this.GetPlayerTokensPlayable(this._teamInfo.OrderPlayer.CurrentPlayer());
    }

    public Tuple<ProtectedToken?, Position> SelectCurrentPlayerMove()
    {
        List<ProtectedToken> protectedTokens = this.GetCurrentPlayerTokensPlayable();

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

        ProtectedToken protectedTokenToPlay = protectedTokens[index];

        Token tokenToPlay = protectedTokenToPlay.GetTokenWithoutVisibility();

        if(!this._table.Empty && this.MatchTokenWithLeftFace(tokenToPlay))
        {
            return new Tuple<ProtectedToken?, Position>(protectedTokenToPlay, Position.Left);
        }

        if(!this._table.Empty && this.MatchTokenWithRightFace(tokenToPlay))
        {
            return new Tuple<ProtectedToken?, Position>(protectedTokenToPlay, Position.Right);
        }
        
        return new Tuple<ProtectedToken?, Position>(protectedTokenToPlay, Position.Middle);
    }

    public void WatchAllPlayers(ProtectedToken token)
    {
        foreach(Player player in this._teamInfo.Players)
        {
            token.Watch(player);
        }
    }

    public void PlayToken(ProtectedToken protectedToken, Position position)
    {
        this.WatchAllPlayers(protectedToken);
        
        Token token = protectedToken.GetTokenWithoutVisibility();

        foreach(Board board in _boards)
        {
            if(board.Contains(protectedToken))
            {
                if(position == Position.Left)
                {
                    if(_table.LeftFace is IFace)
                    {
                        if(token.Faces.Item1.Id == ((IFace)_table.LeftFace).Id)
                        {
                            protectedToken.Rotate();
                            token.Rotate();
                        }

                        Debug.Assert(token.Faces.Item2.Id == ((IFace)_table.LeftFace).Id);

                        this._table.Put(protectedToken, false);
                    }
                }

                if(position == Position.Middle)
                {
                    this._table.Put(protectedToken, false);
                }

                if(position == Position.Right)
                {
                    if(_table.RightFace is IFace)
                    {
                        if(token.Faces.Item2.Id == ((IFace)_table.RightFace).Id)
                        {
                            protectedToken.Rotate();
                            token.Rotate();
                        }

                        Debug.Assert(token.Faces.Item1.Id == ((IFace)_table.RightFace).Id);

                        this._table.Put(protectedToken, true);
                    }
                }

                board.Remove(protectedToken);
            }
        }
    }

    public void ProcessCurrentTurn()
    {
        Tuple<ProtectedToken?, Position> playerMove = this.SelectCurrentPlayerMove();

        if(playerMove.Item2 != Position.Pass)
        {
            Debug.Assert(playerMove.Item1 is ProtectedToken);
            
            if(playerMove.Item1 is ProtectedToken)
            {
                this.PlayToken((ProtectedToken)playerMove.Item1, playerMove.Item2);
            }
        }

        Move move = new Move(this._teamInfo.OrderPlayer.CurrentPlayer(), playerMove.Item1, playerMove.Item2);

        this._history.PlayMove(move);
    }

    public void DistributeTokens(ITokenDealer tokenDealer)
    {
        this._history.Distributed();

        tokenDealer.Distribute(this._box, this._boards);

        foreach(Player player in this._teamInfo.Players)
        {
            foreach(ProtectedToken token in this.GetPlayerBoard(player).GetTokens())
            {
                token.Watch(player);
            }
        }
    }

    public int GetNumberOfContiguousPassedTurns()
    {
        return this._history.GetContiguousPassedTurns();
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
        return this._history.GetNumberOfMoves();
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
        return this._history.GetPlayerTotalPassedTurns(player);
    }

    public bool IsDistributed()
    {
        return this._history.IsDistributed();
    }

    public Move? GetLastMove()
    {
        return this._history.GetLastMove();
    }

    public void ReversePlayerOrder()
    {
        this._teamInfo.OrderPlayer.Reverse();
    }
}