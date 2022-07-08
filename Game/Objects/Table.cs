class Table
{
    private LinkedList<ProtectedToken> _tokens;

    public int Count { get { return this._tokens.Count; } }

    public bool Empty { get { return this._tokens.Count == 0; } }


    public Tuple<ProtectedToken, ProtectedToken>? AvailableTokens
    { 
        get 
        {
            if(this._tokens.First is null || this._tokens.Last is null)
            {
                return null;
            }

            ProtectedToken tokenLeft = this._tokens.First.Value;
            ProtectedToken tokenRight = this._tokens.Last.Value;

            return new Tuple<ProtectedToken, ProtectedToken>(tokenLeft, tokenRight);
        }
    }

    public ProtectedToken? LeftToken
    {
        get 
        {
            Tuple<ProtectedToken, ProtectedToken>? availableTokens = this.AvailableTokens;

            if(availableTokens == null)return null;

            return availableTokens.Item1;
        }
    }

    public ProtectedToken? RightToken
    {
        get 
        {
            Tuple<ProtectedToken, ProtectedToken>? availableTokens = this.AvailableTokens;

            if(availableTokens == null)return null;

            return availableTokens.Item2;
        }
    }

    public void Clear()
    {
        this._tokens.Clear();
    }

    public new string ToString()
    { 
        string text = "";

        foreach(ProtectedToken token in this._tokens)
        {
            text += token.GetTokenWithoutVisibility().ToString() + " ";
        }

        if(this._tokens.Count > 0)
        {
            text.Remove(text.Length-1);
        }

        return text;
    }

    public Table()
    {
        this._tokens = new LinkedList<ProtectedToken>();
    }

    public void Put(ProtectedToken token, bool atLast)
    {
        if(this.AvailableTokens == null)
        {
            this._tokens.AddFirst(token);
        }
        else if(!atLast)
        { 
            this._tokens.AddFirst(token); 
        }
        else
        { 
            this._tokens.AddLast(token); 
        } 
    }

    public List<ProtectedToken> GetProtectedTokens()
    {
        return this._tokens.ToList();
    }

    public List<ProtectedToken> GetTokens()
    {
        return this._tokens.ToList();
    }

    public List<Token> GetTokensWithoutProtection()
    {
        List<Token> tokens = new List<Token>();

        foreach(ProtectedToken token in this.GetProtectedTokens())
        {
            tokens.Add(token.GetTokenWithoutVisibility());
        }

        return tokens;
    }
}