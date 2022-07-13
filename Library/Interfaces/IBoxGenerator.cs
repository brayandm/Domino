interface IBoxGenerator : IBaseInterface, ISelector
{
    List<ProtectedToken> Generate();
}

class ClassicTenBoxGenerator : IBoxGenerator
{
    public List<ProtectedToken> Generate()
    {
        IFaceGenerator faceGenerator = new IntFacesGenerator();
        ITokenGenerator tokenGenerator = new ClassicTokenGenerator();
        IFilterTokenRule filterTokenRule = new NonFilterBoxRules();

        List<Token> tokens = tokenGenerator.Generate(faceGenerator.GetFaces());
        tokens.RemoveAll(x => !filterTokenRule.Apply(x));

        List<ProtectedToken> protectedTokens = new List<ProtectedToken>();
        
        foreach(Token token in tokens)
        {
            protectedTokens.Add(new ProtectedToken(token));
        }

        return protectedTokens;
    }
}

class ClassicSevenBoxGenerator : IBoxGenerator
{
    public List<ProtectedToken> Generate()
    {
        IFaceGenerator faceGenerator = new IntFacesGenerator();
        ITokenGenerator tokenGenerator = new ClassicTokenGenerator();
        IFilterTokenRule filterTokenRule = new NonFilterBoxRules();

        List<Token> tokens = tokenGenerator.Generate((new IntFacesGenerator()).GetFaces(7));
        tokens.RemoveAll(x => !filterTokenRule.Apply(x));

        List<ProtectedToken> protectedTokens = new List<ProtectedToken>();
        
        foreach(Token token in tokens)
        {
            protectedTokens.Add(new ProtectedToken(token));
        }

        return protectedTokens;
    }
}

class ClassicSevenWithoutDoublesBoxGenerator : IBoxGenerator
{
    public List<ProtectedToken> Generate()
    {
        IFaceGenerator faceGenerator = new IntFacesGenerator();
        ITokenGenerator tokenGenerator = new ClassicTokenGenerator();
        IFilterTokenRule filterTokenRule = new WithoutDoblesFilterBoxRules();

        List<Token> tokens = tokenGenerator.Generate((new IntFacesGenerator()).GetFaces(7));
        tokens.RemoveAll(x => !filterTokenRule.Apply(x));

        List<ProtectedToken> protectedTokens = new List<ProtectedToken>();
        
        foreach(Token token in tokens)
        {
            protectedTokens.Add(new ProtectedToken(token));
        }

        return protectedTokens;
    }
}