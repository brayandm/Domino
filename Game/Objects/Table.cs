using System.Diagnostics;

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