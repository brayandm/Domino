using System.Diagnostics;

// Esta clase representa el orden de los turnos de los jugadores
public class OrderPlayer
{
    private List<Player> _playerSequence = new List<Player>();

    private int _currentPlayer = 0;

    private bool _reversed = false;

    // Contructor de la clase, recibe los jugadores y la regla del orden a utilizar
    public OrderPlayer(List<Player> players, IOrderPlayerSequence orderPlayerSecuence)
    {
        this._playerSequence = orderPlayerSecuence.GetOrderPlayersequence(players);
        Debug.Assert(this._playerSequence.Count != 0);
    }

    // Esta funcion hace que se avance al proximo jugador
    public void NextPlayer()
    {
        if(this._reversed)_currentPlayer = (_currentPlayer + _playerSequence.Count - 1) % _playerSequence.Count;
        else _currentPlayer = (_currentPlayer + _playerSequence.Count + 1) % _playerSequence.Count;
    }

    // Esta funcion invierte el orden de los turnos
    public void Reverse()
    {
        this._reversed = !this._reversed;
    }

    // Esta funcion retorna el jugador actual
    public Player CurrentPlayer()
    {
        return _playerSequence[_currentPlayer];
    }

    // Esta funcion reinicializa el orden con el jugador player
    public void RestartWithPlayer(Player player)
    {
        this.RestartOrder();

        while(this.CurrentPlayer() != player)
        {
            this.NextPlayer();
        }
    }

    // Esta funcion reinicializa el orden
    public void RestartOrder()
    {
        this._currentPlayer = 0;
        this._reversed = false;
    }
}