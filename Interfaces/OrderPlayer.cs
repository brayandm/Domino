using System.Diagnostics;

interface IOrderPlayer : IBaseInterface
{
    void Init(List<Player> players);

    void NextPlayer();

    Player CurrentPlayer();
}

class ClassicOrderPlayer : IOrderPlayer
{
    private List<Player> _players = new List<Player>();

    private int _currentPlayer;

    public void Init(List<Player> players)
    {
        Debug.Assert(players.Count != 0);
        this._players = players;
    }

    public Player CurrentPlayer()
    {
        return _players[_currentPlayer];
    }

    public void NextPlayer()
    {
        _currentPlayer = (_currentPlayer + 1) % _players.Count;
    }
}