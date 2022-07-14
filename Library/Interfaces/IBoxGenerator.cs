interface IBoxGenerator : IBaseInterface, ISelector
{
    List<ProtectedToken> Generate();
}

class VariableIntFacesBoxGenerator : IBoxGenerator
{
    private int _numberOfDifferentFaces = 0;

    public VariableIntFacesBoxGenerator()
    {
        while(true)
        {
            Console.WriteLine("Insert the number of different faces to generate (must be greater than zero):\n\n\n\n");
            
            string? entry = Console.ReadLine();

            if(entry is string)
            {
                try
                {
                    this._numberOfDifferentFaces = int.Parse(entry);
                    
                    if(this._numberOfDifferentFaces <= 0)
                    {
                        Console.WriteLine("\n\n\n\nThe inserted number is incorrect, repeat it again\n\n");
                        
                        continue;
                    }

                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("\n\n\n\nThe inserted number is incorrect, repeat it again\n\n");
                }
            }
            else
            {
                Console.WriteLine("\n\n\n\nThe inserted number is incorrect, repeat it again\n\n");
            }
        }

        Console.WriteLine("\n\n\n\n");
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