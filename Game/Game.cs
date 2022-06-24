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
}