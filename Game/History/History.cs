class History
{
    private List<Move> _moves = new List<Move>();

    private int _contiguousPassedTurns = 0;

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

    public IEnumerator<Move> GetEnumerator()
    {
        return (IEnumerator<Move>)this._moves;
    }
}