using System.Diagnostics;

interface IOrderPlayer
{
    void Init(List<Player> players);

    void NextPlayer();

    void Reverse();

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

    public void NextPlayer()
    {
        _currentPlayer = (_currentPlayer + 1) % _players.Count;
    }

    public void Reverse()
    {
        this._players.Reverse();
        this._currentPlayer = this._players.Count - this._currentPlayer - 1;
    }

    public Player CurrentPlayer()
    {
        return _players[_currentPlayer];
    }
}