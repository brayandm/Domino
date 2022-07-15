using System.Diagnostics;

public class HistoryRound
{
    private List<Move> _moves = new List<Move>();

    private int _contiguousPassedTurns = 0;

    private bool _isDistributed = false;

    private Dictionary<Team, int> _teamScore = new Dictionary<Team, int>();

    private Dictionary<Player, int> _playerTotalPassedTurns = new Dictionary<Player, int>();

    private List<Team>_winners = new List<Team>();

    private bool _isRoundEnded = false;

    public void Distributed()
    {
        this._isDistributed = true;
    }

    public void EndRound()
    {
        this._isRoundEnded = true;
    }

    public bool IsRoundEnded()
    {
        return this._isRoundEnded;
    }

    public bool IsDistributed()
    {
        return this._isDistributed;
    }

    public void SetWinners(List<Team> winners)
    {
        this._winners = winners;
    }

    public List<Team> GetWinners()
    {
        return this._winners;
    }

    public void SetTeamScore(Team team, int score)
    {
        if(!this._teamScore.ContainsKey(team))
        {
            this._teamScore.Add(team, score);
        }
    }

    public int GetTeamScore(Team team)
    {
        Debug.Assert(this._teamScore.ContainsKey(team));

        return this._teamScore[team];
    }

    public void PassTurn()
    {
        this._contiguousPassedTurns++;
    }

    public int GetContiguousPassedTurns()
    {
        return this._contiguousPassedTurns;
    }

    public int ResetContiguousPassedTurns()
    {
        return this._contiguousPassedTurns = 0;
    }

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

    private void AddMove(Move move)
    {
        this._moves.Add(move);
    }

    public List<Move> GetMoves()
    {
        return this._moves;
    }

    public Move? GetLastMove()
    {
        if(this._moves.Count == 0)
        {
            return null;
        }
        
        return this._moves.Last();
    }

    public IEnumerator<Move> GetEnumerator()
    {
        return (IEnumerator<Move>)this._moves;
    }

    public int GetNumberOfMoves()
    {
        return this._moves.Count;
    }

    public int GetPlayerTotalPassedTurns(Player player)
    {
        return this._playerTotalPassedTurns.ContainsKey(player) ? this._playerTotalPassedTurns[player] : 0;
    }

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