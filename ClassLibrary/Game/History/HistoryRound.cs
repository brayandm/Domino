using System.Diagnostics;

// Esta clase contiene el historial de una ronda
public class HistoryRound
{
    private List<Move> _moves = new List<Move>();

    private int _contiguousPassedTurns = 0;

    private bool _isDistributed = false;

    private Dictionary<Team, int> _teamScore = new Dictionary<Team, int>();

    private Dictionary<Player, int> _playerTotalPassedTurns = new Dictionary<Player, int>();

    private List<Team>_winners = new List<Team>();

    private bool _isRoundEnded = false;

    // Esta funcion indica a la ronda que ya se hizo la reparticion inicial
    public void Distributed()
    {
        this._isDistributed = true;
    }

    // Esta funcion finaliza la ronda
    public void EndRound()
    {
        this._isRoundEnded = true;
    }

    // Esta funcion retorna si la ronda finalizo o no
    public bool IsRoundEnded()
    {
        return this._isRoundEnded;
    }

    // Esta funcion devuelve si se hizo la reparticion inicial de los tokens
    public bool IsDistributed()
    {
        return this._isDistributed;
    }

    // Esta funcion indica a la ronda los equipos ganadores
    public void SetWinners(List<Team> winners)
    {
        this._winners = winners;
    }

    // Esta funcion retorna los equipos ganadores de la ronda
    public List<Team> GetWinners()
    {
        return this._winners;
    }

    // Esta funcion agrega puntaje a los equipos
    public void SetTeamScore(Team team, int score)
    {
        if(!this._teamScore.ContainsKey(team))
        {
            this._teamScore.Add(team, score);
        }
    }

    // Esta funcion retorna el puntaje de un equipo
    public int GetTeamScore(Team team)
    {
        Debug.Assert(this._teamScore.ContainsKey(team));

        return this._teamScore[team];
    }

    // Esta funcion indica que un jugador se paso de turno
    public void PassTurn()
    {
        this._contiguousPassedTurns++;
    }

    // Esta funcion retorna la cantidad de turnos pasados contiguos
    public int GetContiguousPassedTurns()
    {
        return this._contiguousPassedTurns;
    }

    // Esta funcion resetea los turnos pasados contiguos
    public int ResetContiguousPassedTurns()
    {
        return this._contiguousPassedTurns = 0;
    }

    // Esta funcion juega un nuevo movimiento
    public void PlayMove(Move move)
    {
        if(move.Position == Position.Pass)
        {
            this.PassTurn();

            if(!this._playerTotalPassedTurns.ContainsKey(move.Player))
            {
                this._playerTotalPassedTurns.Add(move.Player, 0);
            }

            this._playerTotalPassedTurns[move.Player]++;
        }
        else
        {
            this.ResetContiguousPassedTurns();
        }

        this.AddMove(move);
    }

    // Esta funcion agrega un nuevo movimiento
    private void AddMove(Move move)
    {
        this._moves.Add(move);
    }


    // Esta funcion retorna los movimientos
    public List<Move> GetMoves()
    {
        return this._moves;
    }

    // Esta funcion obtiene el ultimo movimiento
    public Move? GetLastMove()
    {
        if(this._moves.Count == 0)
        {
            return null;
        }
        
        return this._moves.Last();
    }

    // Esta funcion retorna un enumerator de los movimientos
    public IEnumerator<Move> GetEnumerator()
    {
        return (IEnumerator<Move>)this._moves;
    }

    // Esta funcion retorna el numero de movimientos
    public int GetNumberOfMoves()
    {
        return this._moves.Count;
    }

    // Esta funcion retorna la cantidad de turnos pasados por el jugador player
    public int GetPlayerTotalPassedTurns(Player player)
    {
        return this._playerTotalPassedTurns.ContainsKey(player) ? this._playerTotalPassedTurns[player] : 0;
    }

    // Esta funcion retorna si el ultimo movimiento fue un movimiento de robo
    public bool LastMoveWasDraw()
    {
        Move? move = this.GetLastMove();

        if(move == null)
        {
            return false;
        }

        return move.Position == Position.Draw;
    }
}