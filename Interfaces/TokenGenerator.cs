interface ITokenGenerator : BaseInterface
{
    List<Token> Generate(IFaceGenerator faceGenerator);
    List<Token> Generate(IFaceGenerator faceGenerator, int k);
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
    public List<Token> Generate(IFaceGenerator faceGenerator)
    {
        return AllPair(faceGenerator.Faces);
    }

    public List<Token> Generate(IFaceGenerator faceGenerator, int k)
    {
        return TokenGeneration.KTimesTokens(AllPair(faceGenerator.Faces), k);
    }

    private List<Token> AllPair(List<IFace> faces)
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
    public List<Token> Generate(IFaceGenerator faceGenerator)
    {
        return CyclePair(faceGenerator.Faces);
    }

    public List<Token> Generate(IFaceGenerator faceGenerator, int k)
    {
        return TokenGeneration.KTimesTokens(CyclePair(faceGenerator.Faces), k);
    }

    private List<Token> CyclePair(List<IFace> faces)
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