interface IBoxGenerator : IBaseInterface, ISelector
{
    List<ProtectedToken> Generate();
}

class ClassicBoxGenerator : IBoxGenerator
{
    public List<ProtectedToken> Generate()
    {
        IFaceGenerator faceGenerator = new IntFacesGenerator();
        ITokenGenerator tokenGenerator = new ClassicTokenGenerator();
        IFilterTokenRule filterTokenRule = new NonFilterBoxRules();

        List<Token> tokens = tokenGenerator.Generate(faceGenerator);
        tokens.RemoveAll(x => !filterTokenRule.Apply(x));

        List<ProtectedToken> protectedTokens = new List<ProtectedToken>();
        
        foreach(Token token in tokens)
        {
            protectedTokens.Add(new ProtectedToken(token));
        }

        return protectedTokens;
    }
}