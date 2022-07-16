// Esta interfaz representa el valor de los tokens
public interface ITokenValue : IBaseInterface, ISelector
{
    // Esta funcion retorna el valor de un token
    int GetValue(Token token);
}

// Esta clase representa la regla de suma de caras
public class ClassicSumTokenValue : ITokenValue
{
    public int GetValue(Token token)
    {
        return token.Faces.Item1.Value + token.Faces.Item2.Value;
    }
}

// Esta clase representa la regla de multiplicacion de caras
public class MultiplicationTokenValue : ITokenValue
{
    public int GetValue(Token token)
    {
        return token.Faces.Item1.Value * token.Faces.Item2.Value;
    }
}