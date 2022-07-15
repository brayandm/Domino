public interface IFilterTokenRule : IBaseInterface
{
    bool Apply(Token token);
}

class WithoutDoblesFilterBoxRules : IFilterTokenRule
{
    public bool Apply(Token token)
    {
        return token.Faces.Item1.Id != token.Faces.Item2.Id;
    }
}

class NonFilterBoxRules : IFilterTokenRule
{
    public bool Apply(Token token)
    {
        return true;
    }
}