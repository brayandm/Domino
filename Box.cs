class Box
{
    List<Token> Tokens;
    
    public Box(ITokenGenerator tokenGenerator, IFaceGenerator faceGenerator, IFilterTokenRules filterTokenRules)
    {
        this.Tokens = tokenGenerator.Generate(faceGenerator);
        this.Tokens.RemoveAll(x => !filterTokenRules.Apply(x));
    }

    Token TakeAt(int pos)
    {
        System.Diagnostics.Debug.Assert(0 <= pos && pos < this.Tokens.Count);
        
        Token token = this.Tokens[pos];

        this.Tokens.RemoveAt(pos);

        return token;
    }

    Token TakeRandom()
    {
        return TakeAt(new Random().Next(this.Tokens.Count));
    }

    Token TakeLast()
    {
        return TakeAt(this.Tokens.Count - 1);
    }

    public Token Take()
    {
        System.Diagnostics.Debug.Assert(this.Tokens.Count != 0);

        return TakeRandom();
    }

    public List<Token> Take(int n)
    {
        System.Diagnostics.Debug.Assert(0 <= n && n <= this.Tokens.Count);

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

    void PutAt(Token token, int pos)
    {
        pos = Math.Max(0, Math.Min(this.Tokens.Count, pos));

        this.Tokens.Insert(pos, token);
    }

    void PutRandom(Token token)
    {
        PutAt(token, new Random().Next(this.Tokens.Count + 1));
    }

    void PutLast(Token token)
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

    void Swap(int posA, int posB)
    {
        Token tokenC = this.Tokens[posA];
        this.Tokens[posA] = this.Tokens[posB];
        this.Tokens[posB] = tokenC;
    }

    void Shuffle()
    {
        for(int i = 0 ; i < this.Tokens.Count ; i++)
        {
            Swap(i, new Random().Next(i, this.Tokens.Count));
        }
    }
}