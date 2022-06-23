interface IFilterTokenRules
{
    bool Apply(Token token);
}

class WithoutDoblesFilterBoxRules : IFilterTokenRules
{
    public bool Apply(Token token)
    {
        return token.Faces.Item1.Id != token.Faces.Item2.Id;
    }
}

class NonFilterBoxRules : IFilterTokenRules
{
    public bool Apply(Token token)
    {
        return true;
    }
}