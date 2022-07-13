using System.Diagnostics;

class OrderPlayer
{
    private List<Player> _playerSequence = new List<Player>();

    private int _currentPlayer = 0;

    private bool _reversed = false;

    public OrderPlayer(List<Player> players, IOrderPlayerSequence orderPlayerSecuence)
    {
        this._playerSequence = orderPlayerSecuence.GetOrderPlayersequence(players);
        Debug.Assert(this._playerSequence.Count != 0);
    }

    public void NextPlayer()
    {
        if(this._reversed)_currentPlayer = (_currentPlayer + _playerSequence.Count - 1) % _playerSequence.Count;
        else _currentPlayer = (_currentPlayer + _playerSequence.Count + 1) % _playerSequence.Count;
    }

    public void Reverse()
    {
        this._reversed = !this._reversed;
    }

    public Player CurrentPlayer()
    {
        return _playerSequence[_currentPlayer];
    }

    public void RestartWithPlayer(Player player)
    {
        this.RestartOrder();

        while(this.CurrentPlayer() != player)
        {
            this.NextPlayer();
        }
    }

    public void RestartOrder()
    {
        _currentPlayer = 0;
    }
}