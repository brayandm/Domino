using System.Diagnostics;

class Table
{
    private LinkedList<ProtectedToken> _tokens;

    private string _string;

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

    public new string ToString
    { 
        get 
        {
            string text = "";

            foreach(ProtectedToken token in _tokens)
            {
                text += token.GetTokenWithoutVisibility().ToString() + " ";
            }

            text.Remove(text.Length-1);

            return text;
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
}