interface ITokenGenerator : IBaseInterface
{
    List<Token> Generate(List<IFace> faces);
    List<Token> Generate(List<IFace> faces, int k);
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
        return AllPair(faces);
    }

    public List<Token> Generate(List<IFace> faces, int k)
    {
        return TokenGeneration.KTimesTokens(AllPair(faces), k);
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
    public List<Token> Generate(List<IFace> faces)
    {
        return CyclePair(faces);
    }

    public List<Token> Generate(List<IFace> faces, int k)
    {
        return TokenGeneration.KTimesTokens(CyclePair(faces), k);
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