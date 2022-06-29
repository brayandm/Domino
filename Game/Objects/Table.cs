using System.Diagnostics;

class Table
{
    private LinkedList<ProtectedToken> _tokens;

    public int Count { get { return this._tokens.Count; } }

    public bool Empty { get { return this._tokens.Count == 0; } }


    public Tuple<IFace, IFace>? AvailableFaces
    { 
        get 
        {
            if(this._tokens.First is null || this._tokens.Last is null)
            {
                return null;
            }

            Token tokenLeft = (Token)this._tokens.First.Value.GetTokenWithoutVisibility();
            Token tokenRight = (Token)this._tokens.Last.Value.GetTokenWithoutVisibility();

            return new Tuple<IFace, IFace>(tokenLeft.Faces.Item1, tokenRight.Faces.Item2);
        }
    }

    public IFace? LeftFace
    {
        get 
        {
            Tuple<IFace, IFace>? availableFaces = this.AvailableFaces;

            if(availableFaces == null)return null;

            return availableFaces.Item1;
        }
    }

    public IFace? RightFace
    {
        get 
        {
            Tuple<IFace, IFace>? availableFaces = this.AvailableFaces;

            if(availableFaces == null)return null;

            return availableFaces.Item2;
        }
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
        if(this.AvailableFaces == null)
        {
            this._tokens.AddFirst(token);
        }
        else if(!atLast)
        { 
            Debug.Assert(token.GetTokenWithoutVisibility().Faces.Item2 == this.AvailableFaces.Item1);
            this._tokens.AddFirst(token); 
        }
        else
        { 
            Debug.Assert(this.AvailableFaces.Item2 == token.GetTokenWithoutVisibility().Faces.Item1);
            this._tokens.AddLast(token); 
        } 
    }

    public List<ProtectedToken> GetProtectedTokens()
    {
        return this._tokens.ToList();
    }

    public List<Token> GetTokens()
    {
        List<Token> tokens = new List<Token>();

        foreach(ProtectedToken token in this.GetProtectedTokens())
        {
            tokens.Add(token.GetTokenWithoutVisibility());
        }

        return tokens;
    }
}