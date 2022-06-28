class History
{
    private List<Move> _moves = new List<Move>();

    private int _contiguousPassedTurns = 0;

    private bool _isDistributed = false;

    public void Distributed()
    {
        this._isDistributed = true;
    }

    public bool IsDistributed()
    {
        return this._isDistributed;
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

    public void AddMove(Move move)
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
}