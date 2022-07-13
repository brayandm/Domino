interface ITokenValue : IBaseInterface, ISelector
{
    int GetValue(Token token);
}

class ClassicSumTokenValue : ITokenValue
{
    public int GetValue(Token token)
    {
        return token.Faces.Item1.Value + token.Faces.Item2.Value;
    }
}

class MultiplicationTokenValue : ITokenValue
{
    public int GetValue(Token token)
    {
        return token.Faces.Item1.Value * token.Faces.Item2.Value;
    }
}