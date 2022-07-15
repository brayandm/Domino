// Esta funcion representa una mesa de domino como
//el conjunto defichas organizadas que se encuentran sobre la mesa.
public class Table
{
    private LinkedList<ProtectedToken> _tokens;

    public int Count { get { return this._tokens.Count; } }

    public bool Empty { get { return this._tokens.Count == 0; } }

    // Esta funcion devuelve los elementos(ProtectedToken) 
    //que se encuentran en los extremos de la mesa.
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

    // Esta funcion devuelve el elemento(ProtectedToken) 
    //que se encuentran en el extremo izquierdo de la mesa.
    public ProtectedToken? LeftToken
    {
        get 
        {
            Tuple<ProtectedToken, ProtectedToken>? availableTokens = this.AvailableTokens;

            if(availableTokens == null)return null;

            return availableTokens.Item1;
        }
    }

    // Esta funcion devuelve el elemento(ProtectedToken) 
    //que se encuentran en el extremo derecho de la mesa.
    public ProtectedToken? RightToken
    {
        get 
        {
            Tuple<ProtectedToken, ProtectedToken>? availableTokens = this.AvailableTokens;

            if(availableTokens == null)return null;

            return availableTokens.Item2;
        }
    }

    // Esta funcion elimina todos los elementos(ProtectedToken)
    //que se encuentran sobre la mesa, dejando a la misma vacia.
    public void Clear()
    {
        this._tokens.Clear();
    }

    // Esta funcion retorna un string del objeto
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

    // Esta funcion crea una mesa vacia.
    public Table()
    {
        this._tokens = new LinkedList<ProtectedToken>();
    }

    // Esta funcion coloca un objeto(ProtectedToken) 
    //sobre la mesa, en el extremo deseado.
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

    // Esta funcion devuelve el conjunto de objetos(ProtectedToken)
    //que se encuentran sobre la mesa ordenados en forma de lista. 
    public List<ProtectedToken> GetProtectedTokens()
    {
        return this._tokens.ToList();
    }

    // Esta funcion devuelve el conjunto de fichas sobre la mesa
    //en forma de lista.
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