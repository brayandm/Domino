using System.Diagnostics;

class Box
{
    private List<Token> Tokens;
    
    public Box(ITokenGenerator tokenGenerator, IFaceGenerator faceGenerator, IFilterTokenRules filterTokenRules)
    {
        this.Tokens = tokenGenerator.Generate(faceGenerator);
        this.Tokens.RemoveAll(x => !filterTokenRules.Apply(x));
    }

    private Token TakeAt(int pos)
    {
        Debug.Assert(0 <= pos && pos < this.Tokens.Count);
        
        Token token = this.Tokens[pos];

        this.Tokens.RemoveAt(pos);

        return token;
    }

    private Token TakeRandom()
    {
        return TakeAt(new Random().Next(this.Tokens.Count));
    }

    private Token TakeLast()
    {
        return TakeAt(this.Tokens.Count - 1);
    }

    public Token Take()
    {
        Debug.Assert(this.Tokens.Count != 0);

        return TakeRandom();
    }

    public List<Token> Take(int n)
    {
        Debug.Assert(0 <= n && n <= this.Tokens.Count);

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
        pos = Math.Max(0, Math.Min(this.Tokens.Count, pos));

        this.Tokens.Insert(pos, token);
    }

    private void PutRandom(Token token)
    {
        PutAt(token, new Random().Next(this.Tokens.Count + 1));
    }

    private void PutLast(Token token)
    {
        PutAt(token, this.Tokens.Count);
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
        Token tokenC = this.Tokens[posA];
        this.Tokens[posA] = this.Tokens[posB];
        this.Tokens[posB] = tokenC;
    }

    private void Shuffle()
    {
        for(int i = 0 ; i < this.Tokens.Count ; i++)
        {
            Swap(i, new Random().Next(i, this.Tokens.Count));
        }
    }
}