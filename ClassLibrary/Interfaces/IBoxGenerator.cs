public interface IBoxGenerator : IBaseInterface, ISelector
{
    List<ProtectedToken> Generate();
}

class VariableIntFacesBoxGenerator : IBoxGenerator
{
    private int _numberOfDifferentFaces = 0;

    public VariableIntFacesBoxGenerator()
    {
        bool Validador(string entry)
        {
            try
            {
                int value = int.Parse(entry);
                
                return value > 0;
            }
            catch
            {
                return false;
            }
        }

        Func<string, bool> Func = Validador;

        string entry = ((IGraphicInterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicInterface))).GetEntry("GameRule", "Insert the number of different faces to generate (must be greater than zero):", Func);
    
        this._numberOfDifferentFaces = int.Parse(entry);
    }

    public List<ProtectedToken> Generate()
    {
        IntFacesGenerator faceGenerator = new IntFacesGenerator();
        ITokenGenerator tokenGenerator = new ClassicTokenGenerator();
        IFilterTokenRule filterTokenRule = new NonFilterBoxRules();

        List<Token> tokens = tokenGenerator.Generate(faceGenerator.GetFaces(_numberOfDifferentFaces));
        tokens.RemoveAll(x => !filterTokenRule.Apply(x));

        List<ProtectedToken> protectedTokens = new List<ProtectedToken>();
        
        foreach(Token token in tokens)
        {
            protectedTokens.Add(new ProtectedToken(token));
        }

        return protectedTokens;
    }
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

class ClassicTenBoxGeneratorWithPowerDoubleFacesPassTurn : IBoxGenerator
{
    public List<ProtectedToken> Generate()
    {
        IFaceGenerator faceGenerator = new IntFacesGenerator();
        ITokenGenerator tokenGenerator = new DoubleFacesPassTurnPowerClassicTokenGenerator();
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