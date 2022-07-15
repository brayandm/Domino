using System.Diagnostics;

public class Game
{
    public PowerHandler PowerHandler = new PowerHandler();

    private TeamInfo _teamInfo;

    private List<Board> _boards = new List<Board>();
    private Box _box = new Box();
    private Table _table = new Table();

    private History _history = new History();
    
    private IBoxGenerator _boxGenerator;
    private ITeamsGenerator _teamsGenerator;
    private ITeamOrder _teamOrder;
    private ITokenVisibility _tokenVisibility;
    private IJoinable _joinable;
    private IIdJoinable _idJoinable;
    private IOrderPlayerSequence _orderPlayerSequence;
    private IRoundScoreTeam _roundScoreTeam;
    private IRoundScorePlayer _roundScorePlayer;
    private IRoundWinnerRule _roundWinnerRule;
    private IScoreTeam _scoreTeam;
    private IGameWinnerRule _gameWinnerRule;

    public void NewRoundGame()
    {
        this.PowerHandler.Clear();

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
        this._boxGenerator = (IBoxGenerator)DependencyContainerRegister.Getter.GetInstance(typeof(IBoxGenerator));
        this._teamsGenerator = (ITeamsGenerator)DependencyContainerRegister.Getter.GetInstance(typeof(ITeamsGenerator));
        this._teamOrder = (ITeamOrder)DependencyContainerRegister.Getter.GetInstance(typeof(ITeamOrder));
        this._tokenVisibility = (ITokenVisibility)DependencyContainerRegister.Getter.GetInstance(typeof(ITokenVisibility));
        this._joinable = (IJoinable)DependencyContainerRegister.Getter.GetInstance(typeof(IJoinable));
        this._idJoinable = (IIdJoinable)DependencyContainerRegister.Getter.GetInstance(typeof(IIdJoinable));
        this._orderPlayerSequence = (IOrderPlayerSequence)DependencyContainerRegister.Getter.GetInstance(typeof(IOrderPlayerSequence));
        this._roundScoreTeam = (IRoundScoreTeam)DependencyContainerRegister.Getter.GetInstance(typeof(IRoundScoreTeam));
        this._roundScorePlayer = (IRoundScorePlayer)DependencyContainerRegister.Getter.GetInstance(typeof(IRoundScorePlayer));
        this._roundWinnerRule = (IRoundWinnerRule)DependencyContainerRegister.Getter.GetInstance(typeof(IRoundWinnerRule));
        this._scoreTeam = (IScoreTeam)DependencyContainerRegister.Getter.GetInstance(typeof(IScoreTeam));
        this._gameWinnerRule = (IGameWinnerRule)DependencyContainerRegister.Getter.GetInstance(typeof(IGameWinnerRule));
        
        List<Team> teams = this._teamsGenerator.GetTeams();

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

    public List<Token?> GetBoardNullableTokensVisibleForPlayer(Player player, Board board)
    {
        List<ProtectedToken> tokens = board.GetTokens();

        List<Token?> visibleTokens = new List<Token?>();

        foreach(ProtectedToken token in tokens)
        {
            if(token.IsVisible(player))
            {
                visibleTokens.Add(token.GetTokenWithoutVisibility());
            }
            else
            {
                visibleTokens.Add(null);
            }
        }

        return visibleTokens;
    }

    public List<ProtectedToken> GetBoardProtectedTokensVisibleForPlayer(Player player, Board board)
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

    public List<Tuple<ProtectedToken, Position>> GetPlayerTokensPlayableAndPosition(Player player, IJoinable joinable)
    {
        Board board = this._teamInfo.PlayerBoard[player];
        
        List<Tuple<ProtectedToken, Position>> tokens = new List<Tuple<ProtectedToken, Position>>();

        foreach(ProtectedToken token in GetBoardProtectedTokensVisibleForPlayer(player, board))
        {
            if(this._table.Count == 0)
            {
                tokens.Add(new Tuple<ProtectedToken, Position>(token, Position.Middle));

                continue;
            }

            bool left = false;
            bool right = false;

            if(joinable.IsJoinable(this, token, this._table.LeftToken))
            {
                tokens.Add(new Tuple<ProtectedToken, Position>(token, Position.Left));

                left = true;
            }
            if(joinable.IsJoinable(this, this._table.RightToken, token))
            {
                tokens.Add(new Tuple<ProtectedToken, Position>(token, Position.Right));

                right = true;
            }

            token.Rotate();

            if(!left && joinable.IsJoinable(this, token, this._table.LeftToken))
            {
                tokens.Add(new Tuple<ProtectedToken, Position>(token, Position.Left));
            }
            if(!right && joinable.IsJoinable(this, this._table.RightToken, token))
            {
                tokens.Add(new Tuple<ProtectedToken, Position>(token, Position.Right));
            }
        }

        return tokens;
    }

    public List<Tuple<ProtectedToken, Position>> GetCurrentPlayerTokensPlayableAndPosition(IJoinable joinable)
    {
        return this.GetPlayerTokensPlayableAndPosition(this._teamInfo.OrderPlayer.CurrentPlayer(), joinable);
    }

    public Tuple<ProtectedToken?, Position> SelectCurrentPlayerMove(IJoinable joinable)
    {
        List<Tuple<ProtectedToken, Position>> protectedTokens = this.GetCurrentPlayerTokensPlayableAndPosition(joinable);

        Player player = this._teamInfo.OrderPlayer.CurrentPlayer();

        List<Tuple<Token, Position>> tokens = new List<Tuple<Token, Position>>();

        foreach(Tuple<ProtectedToken, Position> protectedToken in protectedTokens)
        {
            Debug.Assert(protectedToken.Item1.IsVisible(player));

            tokens.Add(new Tuple<Token, Position>(protectedToken.Item1.GetTokenWithoutVisibility(), protectedToken.Item2));
        }

        int index = player.Strategy.ChooseTokenIndex(tokens, player, this._teamInfo.Teams, this.GetPlayersAndBoardTokensVisibleByPlayer(player), this._table.GetTokensWithoutProtection(), this.GetPositionMiddle());

        if(tokens.Count == 0)
        {
            return new Tuple<ProtectedToken?, Position>(null, Position.Pass);
        }

        return new Tuple<ProtectedToken?, Position>(protectedTokens[index].Item1, protectedTokens[index].Item2);
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
        this.PowerHandler.AddPowers(token.GetTokenWithoutVisibility().Powers);

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
        return this._gameWinnerRule.GetWinners(this);
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
            List<ProtectedToken> protectedTokens = this._table.GetProtectedTokens();

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