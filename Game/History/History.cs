class History
{
    private List<Move> _moves = new List<Move>();

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