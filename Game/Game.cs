using System.Diagnostics;

class Game
{
    private PlayerInfo _playerInfo;

    private List<Board> _boards;
    private Box _box;
    private Table _table;

    private History _history;

    public Game(ITokenGenerator tokenGenerator, IFaceGenerator faceGenerator, IFilterTokenRules filterTokenRules, IPlayerGenerator playerGenerator, IOrderPlayer orderPlayer)
    {
        List<Player> players = playerGenerator.GetPlayers();
        this._boards = new List<Board>();
        
        for(int i = 0 ; i < players.Count ; i++)
        {
            this._boards.Add(new Board());
        }

        this._playerInfo = new PlayerInfo(players, _boards, orderPlayer);
        this._boards = new List<Board>();
        this._box = new Box(tokenGenerator, faceGenerator, filterTokenRules);
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
        if(MatchTokenWithFace(token, faces.Item1) || MatchTokenWithFace(token, faces.Item2))return true;
        return false;
    }

    public bool MatchTokenWithLeftFace(Token token)
    {
        Tuple<IFace, IFace>? availableFaces = this._table.AvailableFaces;

        if(availableFaces == null)
        {
            return true;
        }

        return MatchTokenWithFace(token, availableFaces.Item1);
    }

    public bool MatchTokenWithRightFace(Token token)
    {
        Tuple<IFace, IFace>? availableFaces = this._table.AvailableFaces;

        if(availableFaces == null)
        {
            return true;
        }

        return MatchTokenWithFace(token, availableFaces.Item2);
    }

    public bool MatchTokenWithBothFaces(Token token)
    {
        return MatchTokenWithLeftFace(token) && MatchTokenWithRightFace(token);
    }

    public List<ProtectedToken> GetPlayerTokensPlayable(Player player)
    {
        Board board = this._playerInfo.PlayerBoard[player];
        
        List<ProtectedToken> tokens = new List<ProtectedToken>();

        foreach(ProtectedToken token in GetBoardTokensVisibleForPlayer(player, board))
        {
            if(MatchTokenWithBothFaces(token.GetTokenWithoutVisibility()))
            {
                tokens.Add(token);
            }
        }

        return tokens;
    }

    public List<ProtectedToken> GetCurrentPlayerTokensPlayable()
    {
        return GetPlayerTokensPlayable(this._playerInfo.OrderPlayer.CurrentPlayer());
    }

    public Tuple<ProtectedToken?, Position> SelectCurrentPlayerMove()
    {
        List<ProtectedToken> protectedTokens = GetCurrentPlayerTokensPlayable();

        if(protectedTokens.Count == 0)
        {
            return new Tuple<ProtectedToken?, Position>(null, Position.Pass);
        }

        Player player = this._playerInfo.OrderPlayer.CurrentPlayer();

        List<Token> tokens = new List<Token>();

        foreach(ProtectedToken protectedToken in protectedTokens)
        {
            Debug.Assert(protectedToken.IsVisible(player));

            tokens.Add(protectedToken.GetTokenWithoutVisibility());
        }

        int index = player.Strategy.ChooseTokenIndex(tokens);

        ProtectedToken protectedTokenToPlay = protectedTokens[index];

        Token tokenToPlay = protectedTokenToPlay.GetTokenWithoutVisibility();

        if(!this._table.Empty && MatchTokenWithLeftFace(tokenToPlay))
        {
            return new Tuple<ProtectedToken?, Position>(protectedTokenToPlay, Position.Left);
        }

        if(!this._table.Empty && MatchTokenWithRightFace(tokenToPlay))
        {
            return new Tuple<ProtectedToken?, Position>(protectedTokenToPlay, Position.Right);
        }
        
        return new Tuple<ProtectedToken?, Position>(protectedTokenToPlay, Position.Middle);
    }

    public void PlayToken(ProtectedToken protectedToken, Position position)
    {
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
                        }

                        Debug.Assert(token.Faces.Item1.Id == ((IFace)_table.RightFace).Id);

                        this._table.Put(protectedToken, true);
                    }
                }

                board.Remove(protectedToken);
            }
        }
    }

    public void DistributeTokens(ITokenDealer tokenDealer)
    {
        tokenDealer.Distribute(this._box, this._boards);
    }
}