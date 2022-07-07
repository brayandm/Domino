class States
{
    public static bool Identity(Game game)
    {
        return true;
    }

    public static bool IsAllPlayersPassed(Game game)
    {
        if(game.GetNumberOfContiguousPassedTurns() >= game.GetNumberOfPlayers())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool IsLastPlayerPassed(Game game)
    {
        if(game.GetNumberOfContiguousPassedTurns() != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool IsNotLastPlayerPassed(Game game)
    {
        return !IsLastPlayerPassed(game);
    }

    public static bool CurrentPlayerPlayedAllTokens(Game game)
    {
        if(game.CurrentPlayerPlayedAllTokens() == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool IsRoundGameOver(Game game)
    {
        IRoundFinalizationRule roundFinalizationRule = (IRoundFinalizationRule)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(IRoundFinalizationRule));
        
        return roundFinalizationRule.IsRoundGameOver(game);
    }

    public static bool IsConditionMetToReverse(Game game)
    {
        IReversePlayerOrder reversePlayerOrder = (IReversePlayerOrder)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(IReversePlayerOrder));
        
        return reversePlayerOrder.IsConditionMet(game);
    }
}