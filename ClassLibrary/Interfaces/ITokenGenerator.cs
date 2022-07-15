public interface ITokenGenerator : IBaseInterface
{
    List<Token> Generate(List<IFace> faces);

    List<Token> Generate(List<IFace> faces, int k)
    {
        return TokenGeneration.KTimesTokens(this.Generate(faces), k);
    }
}

static class TokenGeneration
{
    public static List<Token> KTimesTokens(List<Token> tokens, int k)
    {
        List<Token> result = new List<Token>();

        for(int i = 0 ; i < k ; i++)
        {
            result.Concat(tokens);
        }

        return result;
    }
}

class ClassicTokenGenerator : ITokenGenerator
{
    public List<Token> Generate(List<IFace> faces)
    {
        List<Token> tokens = new List<Token>();

        for(int i = 0 ; i < faces.Count ; i++)
        {
            for(int j = i ; j < faces.Count ; j++)
            {
                IFace faceA = faces[i];
                IFace faceB = faces[j];

                tokens.Add(new Token(faceA, faceB));
            }
        }

        return tokens;
    }
}

class CycleTokenGenerator : ITokenGenerator
{
    public List<Token> Generate(List<IFace> faces)
    {
        List<Token> tokens = new List<Token>();

        for(int i = 0 ; i < faces.Count ; i++)
        {
            IFace faceA = faces[i];
            IFace faceB = faces[(i + 1) % faces.Count];

            tokens.Add(new Token(faceA, faceB));
        }

        return tokens;
    }
}

class DoubleFacesPassTurnPowerClassicTokenGenerator : ITokenGenerator
{
    public List<Token> Generate(List<IFace> faces)
    {
        List<Token> tokens = new List<Token>();

        for(int i = 0 ; i < faces.Count ; i++)
        {
            for(int j = i ; j < faces.Count ; j++)
            {
                IFace faceA = faces[i];
                IFace faceB = faces[j];

                if(faceA.Id == faceB.Id)
                {
                    tokens.Add(new Token(faceA, faceB, new PassNextPlayerTurnPower()));
                }
                else
                {
                    tokens.Add(new Token(faceA, faceB));
                }
            }
        }

        return tokens;
    }
}

class DoubleZeroEndRoundPowerClassicTokenGenerator : ITokenGenerator
{
    public List<Token> Generate(List<IFace> faces)
    {
        List<Token> tokens = new List<Token>();

        for(int i = 0 ; i < faces.Count ; i++)
        {
            for(int j = i ; j < faces.Count ; j++)
            {
                IFace faceA = faces[i];
                IFace faceB = faces[j];

                if(faceA.Id == "0" && faceB.Id == "0")
                {
                    tokens.Add(new Token(faceA, faceB, new EndRoundPower()));
                }
                else
                {
                    tokens.Add(new Token(faceA, faceB));
                }
            }
        }

        return tokens;
    }
}