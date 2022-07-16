// Esta interfaz representa la regla de filtrado de tokens
public interface IFilterTokenRule : IBaseInterface
{
    // Esta funcion representa si el token cumple la condicion
    bool Apply(Token token);
}

// Esta clase representa un filtro sin dobles
public class WithoutDoblesFilterBoxRules : IFilterTokenRule
{
    public bool Apply(Token token)
    {
        return token.Faces.Item1.Id != token.Faces.Item2.Id;
    }
}

// Esta clase representa un filtro que todo token siempre cumplira
public class NonFilterBoxRules : IFilterTokenRule
{
    public bool Apply(Token token)
    {
        return true;
    }
}