// Esta interfaz representa la condicion de inversion del orden
public interface IReversePlayerOrder : IBaseInterface, ISelector
{
    // Esta funcion indica si se cumple la condicion
    bool IsConditionMet(Game game);
}

// Esta clase representa que si un jugador se pasa se invierte el orden
public class ReverseWithPass : IReversePlayerOrder
{
    public bool IsConditionMet(Game game)
    {
        Move? move = game.GetLastMove();

        if(move != null && move.Position == Position.Pass)
        {
            return true;
        }

        return false;
    }
}

// Esta clase representa que si un jugador juega un doble se invierte el orden
public class ReverseWithDoubles : IReversePlayerOrder
{
    public bool IsConditionMet(Game game)
    {
        Move? move = game.GetLastMove();

        if(move != null && move.Token != null && move.Token.GetTokenWithoutVisibility().IsDouble())
        {
            return true;
        }

        return false;
    }
}

// Esta clase representa que nunca se invierte el orden
public class NoReverse : IReversePlayerOrder
{
    public bool IsConditionMet(Game game)
    {
        return false;
    }
}