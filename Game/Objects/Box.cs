using System.Diagnostics;

class Box
{
    private List<ProtectedToken> _tokens = new List<ProtectedToken>();

    public int Count { get { return this._tokens.Count; } }

    public Box()
    {
        this._tokens = new List<ProtectedToken>();
    }

    public void Init(IBoxGenerator boxGenerator)
    {
        this._tokens = boxGenerator.Generate();
    }

    public void Clear()
    {
        this._tokens.Clear();
    }

    private ProtectedToken TakeAt(int pos)
    {
        Debug.Assert(0 <= pos && pos < this._tokens.Count);
        
        ProtectedToken token = this._tokens[pos];

        this._tokens.RemoveAt(pos);

        return token;
    }

    private ProtectedToken TakeRandom()
    {
        return TakeAt(new Random().Next(this._tokens.Count));
    }

    private ProtectedToken TakeLast()
    {
        return TakeAt(this._tokens.Count - 1);
    }

    public ProtectedToken Take()
    {
        Debug.Assert(this._tokens.Count != 0);

        return TakeRandom();
    }

    public List<ProtectedToken> Take(int n)
    {
        Debug.Assert(0 <= n && n <= this._tokens.Count);

        List<ProtectedToken> tokens = new List<ProtectedToken>();

        for(int i = 0 ; i < n ; i++)
        {
            tokens.Add(TakeRandom());
        }

        return tokens;
    }

    private void PutAt(ProtectedToken token, int pos)
    {
        pos = Math.Max(0, Math.Min(this._tokens.Count, pos));

        this._tokens.Insert(pos, token);
    }

    private void PutRandom(ProtectedToken token)
    {
        PutAt(token, new Random().Next(this._tokens.Count + 1));
    }

    private void PutLast(ProtectedToken token)
    {
        PutAt(token, this._tokens.Count);
    }

    public void Put(ProtectedToken token)
    {
        PutRandom(token);
    }

    public void Put(List<ProtectedToken> tokens)
    {
        foreach(ProtectedToken token in tokens)
        {
            PutRandom(token);
        }
    }

    private void Swap(int posA, int posB)
    {
        ProtectedToken tokenC = this._tokens[posA];
        this._tokens[posA] = this._tokens[posB];
        this._tokens[posB] = tokenC;
    }

    private void Shuffle()
    {
        for(int i = 0 ; i < this._tokens.Count ; i++)
        {
            Swap(i, new Random().Next(i, this._tokens.Count));
        }
    }
}