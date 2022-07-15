public interface IReversePlayerOrder : IBaseInterface, ISelector
{
    bool IsConditionMet(Game game);
}

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

public class NoReverse : IReversePlayerOrder
{
    public bool IsConditionMet(Game game)
    {
        return false;
    }
}