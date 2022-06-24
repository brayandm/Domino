class Board
{
    private List<ProtectedToken> _tokens;

    public int Count { get { return this._tokens.Count; } }

    public Board()
    {
        this._tokens = new List<ProtectedToken>();
    }

    public Board(List<ProtectedToken> tokens)
    {
        this._tokens = tokens;
    }

    public List<ProtectedToken> GetTokens()
    {
        return this._tokens;
    }

    private void OrderBy(Func<ProtectedToken,int> keySelector)
    {
        this._tokens.OrderBy(keySelector);
    }

    private void Sort()
    {
        this._tokens.OrderBy(x => x.GetTokenWithoutVisibility().GetTotalValue());
    }

    public IEnumerator<ProtectedToken> GetEnumerator()
    {
        return (IEnumerator<ProtectedToken>)this._tokens;
    }

    public void Add(ProtectedToken token)
    {
        this._tokens.Add(token);
    }

    public void Add(List<ProtectedToken> tokens)
    {
        foreach(ProtectedToken token in tokens)
        {
            this.Add(token);
        } 
    }

    public void Clear()
    {
        this._tokens.Clear();
    }

    public bool Contains(ProtectedToken token)
    {
        return this._tokens.Contains(token);
    } 

    public void CopyTo(List<ProtectedToken> tokens, int startIndex)
    {
        this._tokens.CopyTo(tokens.ToArray(), startIndex);
    }

    public bool Remove(ProtectedToken token)
    {
        return this._tokens.Remove(token);
    }

    public void Remove(List<ProtectedToken> tokens)
    {
        foreach(ProtectedToken token in tokens)
        {
            this.Remove(token);
        } 
    }
}