class Table
{
    private LinkedList<ProtectedToken> _tokens;

    private string _string;

    public Tuple<IFace, IFace>? AvailableFaces
    { 
        get 
        {
            if(this._tokens.First is null || this._tokens.Last is null)
            {
                return null;
            }

            Token t1 = (Token)this._tokens.First.Value.GetTokenWithoutVisibility();
            Token t2 = (Token)this._tokens.Last.Value.GetTokenWithoutVisibility();

            return new Tuple<IFace, IFace>(t1.Faces.Item1, t2.Faces.Item2);
        }
    }

    public Table()
    {
        this._tokens = new LinkedList<ProtectedToken>();
        this._string = "";
    }

    public void Put(ProtectedToken token, bool atLast)
    {
        if(this.AvailableFaces == null)
        {
            this._string = token.GetTokenWithoutVisibility().ToString();
        }
        else if(atLast)
        { 
            this._tokens.AddLast(token); 
            this._string = this._string + " " + token.GetTokenWithoutVisibility().ToString(); 
        }
        else
        { 
            this._tokens.AddFirst(token); 
            this._string = token.GetTokenWithoutVisibility().ToString() + " " + this._string;
        } 
    }
}