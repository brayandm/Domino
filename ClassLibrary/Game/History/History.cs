using System.Diagnostics;

// Esta clase contiene el historial del juego
public class History
{
    private List<HistoryRound> _historyRounds = new List<HistoryRound>();

    private bool _isEnded = false;

    // Esta funcion crea una nueva historia de una ronda
    public void NewHistoryRound()
    {
        this._historyRounds.Add(new HistoryRound());
    }

    // Esta funcion retorna la historia de la ronda actual
    public HistoryRound GetCurrentHistoryRound()
    {
        return this._historyRounds.Count > 0 ? this._historyRounds.Last() : new HistoryRound();
    }

    // Esta funcion retorna las historias de las rondas
    public List<HistoryRound> GetHistoryRounds()
    {
        return this._historyRounds;
    }

    // Esta funcion retorna el numero de rondas hasta el momento
    public int GetNumberOfRounds()
    {
        return this._historyRounds.Count;
    }

    // Esta funcion termina el juego
    public void SetGameToEnded()
    {
        this._isEnded = true;
    }

    // Esta funcion retorna si el juego termino o no
    public bool IsGameEnded()
    {
        return this._isEnded;
    }
}