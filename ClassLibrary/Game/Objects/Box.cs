using System.Diagnostics;

// Esta clase representa la caja, donde se guardan
//todas las piezas(ProtectedToken) al inicio del juego.
public class Box
{
    private List<ProtectedToken> _tokens = new List<ProtectedToken>();

    public int Count { get { return this._tokens.Count; } }

    public Box()
    {
        this._tokens = new List<ProtectedToken>();
    }

    // Esta funcion genera el conjunto de elementos
    //(ProtectedToken) que conforman la caja y los guarda.
    public void Init(IBoxGenerator boxGenerator)
    {
        this._tokens = boxGenerator.Generate();
    }

    // Esta funcion elimina todos los elementos(ProtectedToken)
    //de la caja.
    public void Clear()
    {
        this._tokens.Clear();
    }

    // Esta funcion elimina el elemento(ProtectedToken)
    //en la posicion pos de la caja y lo devuelve.
    private ProtectedToken TakeAt(int pos)
    {
        if(pos < 0 || this._tokens.Count <= pos)
        {
            throw new TokenUnavailabilityException();
        }
        
        ProtectedToken token = this._tokens[pos];

        this._tokens.RemoveAt(pos);

        return token;
    }

    // Esta funcion elimina un elemento(ProtectedToken)
    //aleatorio de la caja y lo devuelve.
    private ProtectedToken TakeRandom()
    {
        return TakeAt(new Random().Next(this._tokens.Count));
    }

    // Esta funcion elimina ultimo 
    //elemento(ProtectedToken) de la caja y lo devuelve.
    private ProtectedToken TakeLast()
    {
        return TakeAt(this._tokens.Count - 1);
    }

    // Esta funcion elimina un elemento(ProtectedToken)
    //aleatorio de la caja y lo devuelve.
    public ProtectedToken Take()
    {
        if(this._tokens.Count == 0)
        {
            throw new TokenUnavailabilityException();
        }

        return TakeRandom();
    }

    // Esta funcion elimina n elementos(ProtectedToken)
    //aleatorios de la caja y los devuelve como una lista.
    public List<ProtectedToken> Take(int n)
    {
        if(n < 0 || this._tokens.Count < n)
        {
            throw new TokenUnavailabilityException();
        }

        List<ProtectedToken> tokens = new List<ProtectedToken>();

        for(int i = 0 ; i < n ; i++)
        {
            tokens.Add(TakeRandom());
        }

        return tokens;
    }

    // Esta funcion inserta el elemento(ProtectedToken)
    //"token" en la psocion pos en la caja.
    private void PutAt(ProtectedToken token, int pos)
    {
        pos = Math.Max(0, Math.Min(this._tokens.Count, pos));

        this._tokens.Insert(pos, token);
    }

    // Esta funcion inserta el elemento(ProtectedToken)
    //"token" en una psocion aleatoria en la caja.
    private void PutRandom(ProtectedToken token)
    {
        PutAt(token, new Random().Next(this._tokens.Count + 1));
    }

    // Esta funcion inserta el elemento(ProtectedToken)
    //"token" al final de la caja.
    private void PutLast(ProtectedToken token)
    {
        PutAt(token, this._tokens.Count);
    }

    // Esta funcion inserta el elemento(ProtectedToken)
    //"token" en una psocion aleatoria en la caja.
    public void Put(ProtectedToken token)
    {
        PutRandom(token);
    }

    // Esta funcion inserta los elementos(ProtectedToken)
    //"tokens" de forma aleatoria en la caja.
    public void Put(List<ProtectedToken> tokens)
    {
        foreach(ProtectedToken token in tokens)
        {
            PutRandom(token);
        }
    }

    // Esta funcion intercambia de posicon dos
    //elementos(ProtectedToken) dentro de la caja.
    private void Swap(int posA, int posB)
    {
        ProtectedToken tokenC = this._tokens[posA];
        this._tokens[posA] = this._tokens[posB];
        this._tokens[posB] = tokenC;
    }

    // Esta funcion revuelve los elementos(ProtectedToken)
    //dentro de la caja.
    private void Shuffle()
    {
        for(int i = 0 ; i < this._tokens.Count ; i++)
        {
            Swap(i, new Random().Next(i, this._tokens.Count));
        }
    }
}