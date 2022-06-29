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

    public static bool IsGameOver(Game game)
    {
        IFinalizationRule finalizationRule = (IFinalizationRule)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(IFinalizationRule));
        
        return finalizationRule.IsGameOver(game);
    }

    public static bool IsConditionMetToReverse(Game game)
    {
        IReversePlayerOrder reversePlayerOrder = (IReversePlayerOrder)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(IReversePlayerOrder));
        
        return reversePlayerOrder.IsConditionMet(game);
    }
}