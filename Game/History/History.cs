class History
{
    private List<Move> _moves = new List<Move>();

    private int _passTurns = 0;

    public void PassTurn()
    {
        this._passTurns++;
    }

    public int GetPassTurns()
    {
        return this._passTurns;
    }

    public void Add(Move move)
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