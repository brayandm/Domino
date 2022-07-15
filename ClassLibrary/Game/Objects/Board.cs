// Esta clase representa la mano del jugador.
public class Board
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

    // Esta funcion devuelve el conjunto de tokens
    //protegidos en la mano del jugador.
    public List<ProtectedToken> GetTokens()
    {
        return this._tokens;
    }

    // Esta funcion ordena los ProtectedToken de la mano
    //mediante el criterio de comparacion kaySelector.
    private void OrderBy(Func<ProtectedToken,int> keySelector)
    {
        this._tokens.OrderBy(keySelector);
    }

    // Esta funcion ordena los ProtectedToken de la mano
    //de acuerdo a sus valores.
    private void Sort()
    {
        this._tokens.OrderBy(x => x.GetTokenWithoutVisibility().GetValue());
    }

    // Esta funcion devuelve un IEnumerator, el cual
    //contiene los ProtectedToken que conforman la mano.
    public IEnumerator<ProtectedToken> GetEnumerator()
    {
        return (IEnumerator<ProtectedToken>)this._tokens;
    }

    // Esta funcion agrega un token a la mano.
    public void Add(ProtectedToken token)
    {
        this._tokens.Add(token);
    }

    // Esta funcion agrega una lista de tokens a la mano.
    public void Add(List<ProtectedToken> tokens)
    {
        foreach(ProtectedToken token in tokens)
        {
            this.Add(token);
        } 
    }

    // Esta funcion vacia la mano; es decir
    //elimina todos los ProtectedToken de la mano.
    public void Clear()
    {
        this._tokens.Clear();
    }

    // Esta funcion comprueba si la mano contiene
    //a un Protected Token en especifico; en ese caso devuelve true,
    //en caso contrario devuelve false.
    public bool Contains(ProtectedToken token)
    {
        return this._tokens.Contains(token);
    } 

    // Esta funcion toma todos los elementos de la mano
    //y los inserta en la lista tokens a partir del indice seleccionado.
    public void CopyTo(List<ProtectedToken> tokens, int startIndex)
    {
        this._tokens.CopyTo(tokens.ToArray(), startIndex);
    }

    // Esta funcion elimina un elemento(ProtectedToken)
    //de la mano.
    public bool Remove(ProtectedToken token)
    {
        return this._tokens.Remove(token);
    }

    // Esta funcion elimina un conjunto de elementos(ProtectedToken)
    //de la mano
    public void Remove(List<ProtectedToken> tokens)
    {
        foreach(ProtectedToken token in tokens)
        {
            this.Remove(token);
        } 
    }
}