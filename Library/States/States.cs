class States
{
    public static bool Identity(Game game)
    {
        return true;
    }

    public static bool IsAllPlayersPassed(Game game)
    {
        return game.GetNumberOfContiguousPassedTurns() >= game.GetNumberOfPlayers();
    }

    public static bool IsLastPlayerPassed(Game game)
    {
        return game.GetNumberOfContiguousPassedTurns() != 0;
    }

    public static bool IsNotLastPlayerPassed(Game game)
    {
        return !IsLastPlayerPassed(game);
    }

    public static bool CurrentPlayerPlayedAllTokens(Game game)
    {
        return game.CurrentPlayerPlayedAllTokens() == true;
    }

    public static bool IsRoundGameOver(Game game)
    {
        if(game.PowerHandler.GetActivity("EndRoundPower") || game.PowerHandler.GetActivity("EndGamePower"))
        {
            return true;
        }

        IRoundFinalizationRule roundFinalizationRule = (IRoundFinalizationRule)DependencyContainerRegister.Register.Organizer.CreateInstanceFromDefault(typeof(IRoundFinalizationRule));
        
        return roundFinalizationRule.IsRoundGameOver(game);
    }

    public static bool IsGameOver(Game game)
    {
        if(game.PowerHandler.GetActivity("EndGamePower"))
        {
            return true;
        }

        IGameFinalizable gameFinalizable = (IGameFinalizable)DependencyContainerRegister.Register.Organizer.CreateInstanceFromDefault(typeof(IGameFinalizable));
        
        return gameFinalizable.IsGameFinalizable(game);
    }

    public static bool IsConditionMetToReverse(Game game)
    {
        IReversePlayerOrder reversePlayerOrder = (IReversePlayerOrder)DependencyContainerRegister.Register.Organizer.CreateInstanceFromDefault(typeof(IReversePlayerOrder));
        
        return reversePlayerOrder.IsConditionMet(game);
    }

    public static bool IsDrawable(Game game)
    {
        IDrawable drawable = (IDrawable)DependencyContainerRegister.Register.Organizer.CreateInstanceFromDefault(typeof(IDrawable));

        return drawable.IsDrawable(game);
    }
}