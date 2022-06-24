using System.Diagnostics;

class Box
{
    private List<Token> _tokens;
    
    public Box(ITokenGenerator tokenGenerator, IFaceGenerator faceGenerator, IFilterTokenRules filterTokenRules)
    {
        this._tokens = tokenGenerator.Generate(faceGenerator);
        this._tokens.RemoveAll(x => !filterTokenRules.Apply(x));
    }

    private Token TakeAt(int pos)
    {
        Debug.Assert(0 <= pos && pos < this._tokens.Count);
        
        Token token = this._tokens[pos];

        this._tokens.RemoveAt(pos);

        return token;
    }

    private Token TakeRandom()
    {
        return TakeAt(new Random().Next(this._tokens.Count));
    }

    private Token TakeLast()
    {
        return TakeAt(this._tokens.Count - 1);
    }

    public Token Take()
    {
        Debug.Assert(this._tokens.Count != 0);

        return TakeRandom();
    }

    public List<Token> Take(int n)
    {
        Debug.Assert(0 <= n && n <= this._tokens.Count);

        List<Token> tokens = new List<Token>();

        for(int i = 0 ; i < n ; i++)
        {
            Token? token = TakeRandom();

            if(token is Token)
            {
                tokens.Add((Token)token);
            }
        }

        return tokens;
    }

    private void PutAt(Token token, int pos)
    {
        pos = Math.Max(0, Math.Min(this._tokens.Count, pos));

        this._tokens.Insert(pos, token);
    }

    private void PutRandom(Token token)
    {
        PutAt(token, new Random().Next(this._tokens.Count + 1));
    }

    private void PutLast(Token token)
    {
        PutAt(token, this._tokens.Count);
    }

    public void Put(Token token)
    {
        PutRandom(token);
    }

    public void Put(List<Token> tokens)
    {
        foreach(Token token in tokens)
        {
            PutRandom(token);
        }
    }

    private void Swap(int posA, int posB)
    {
        Token tokenC = this._tokens[posA];
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