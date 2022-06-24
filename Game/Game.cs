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

    public bool MatchTokenWithFaces(Token token, Tuple<IFace, IFace> faces)
    {
        if(token.Faces.Item1.Id == faces.Item1.Id)return true;
        if(token.Faces.Item1.Id == faces.Item2.Id)return true;
        if(token.Faces.Item2.Id == faces.Item1.Id)return true;
        if(token.Faces.Item2.Id == faces.Item2.Id)return true;
        return false;
    }

    public List<ProtectedToken> GetPlayerTokensPlayable(Player player)
    {
        Board board = this._playerInfo.PlayerBoard[player];

        Tuple<IFace, IFace>? availableFaces = this._table.AvailableFaces;

        if(availableFaces == null)
        {
            return GetBoardTokensVisibleForPlayer(player, board);
        }
        
        List<ProtectedToken> tokens = new List<ProtectedToken>();

        foreach(ProtectedToken token in GetBoardTokensVisibleForPlayer(player, board))
        {
            if(MatchTokenWithFaces(token.GetTokenWithoutVisibility(), availableFaces))
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

    public ProtectedToken? SelectCurrentPlayerMove()
    {
        List<ProtectedToken> protectedTokens = GetCurrentPlayerTokensPlayable();

        if(protectedTokens.Count == 0)
        {
            return null;
        }

        Player player = this._playerInfo.OrderPlayer.CurrentPlayer();

        List<Token> tokens = new List<Token>();

        foreach(ProtectedToken protectedToken in protectedTokens)
        {
            Debug.Assert(protectedToken.IsVisible(player));

            tokens.Add(protectedToken.GetTokenWithoutVisibility());
        }

        int index = player.Strategy.ChooseTokenIndex(tokens);

        return protectedTokens[index];
    }
}