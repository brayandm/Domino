// Esta clase representa una coleccion de estados
public class States
{
    // Esta clase representa el estado identidad
    public static bool Identity(Game game)
    {
        return true;
    }

    // Esta clase representa el estado de que todos los jugadores se pasaron
    public static bool IsAllPlayersPassed(Game game)
    {
        return game.GetNumberOfContiguousPassedTurns() >= game.GetNumberOfPlayers();
    }

    // Esta clase representa el estado de que el ultimo jugador se paso
    public static bool IsLastPlayerPassed(Game game)
    {
        return game.GetNumberOfContiguousPassedTurns() != 0;
    }

    // Esta clase representa el estado de que el ultimo jugador no se paso
    public static bool IsNotLastPlayerPassed(Game game)
    {
        return !IsLastPlayerPassed(game);
    }

    // Esta funcion retorna si el jugador actual jugo todas sus fichas
    public static bool CurrentPlayerPlayedAllTokens(Game game)
    {
        return game.CurrentPlayerPlayedAllTokens() == true;
    }

    // Esta funcion retorna si la ronda termino
    public static bool IsRoundGameOver(Game game)
    {
        if(game.PowerHandler.GetActivity("EndRoundPower") || game.PowerHandler.GetActivity("EndGamePower"))
        {
            return true;
        }

        IRoundFinalizationRule roundFinalizationRule = (IRoundFinalizationRule)DependencyContainerRegister.Getter.GetInstance(typeof(IRoundFinalizationRule));
        
        return roundFinalizationRule.IsRoundGameOver(game);
    }

    // Esta funcion retorna el el juego termino
    public static bool IsGameOver(Game game)
    {
        if(game.PowerHandler.GetActivity("EndGamePower"))
        {
            return true;
        }

        IGameFinalizable gameFinalizable = (IGameFinalizable)DependencyContainerRegister.Getter.GetInstance(typeof(IGameFinalizable));
        
        return gameFinalizable.IsGameFinalizable(game);
    }

    // Esta funcion retorna si la condicion para invertir el orden de los jugadores se cumple
    public static bool IsConditionMetToReverse(Game game)
    {
        IReversePlayerOrder reversePlayerOrder = (IReversePlayerOrder)DependencyContainerRegister.Getter.GetInstance(typeof(IReversePlayerOrder));
        
        return reversePlayerOrder.IsConditionMet(game);
    }

    // Esta funcion retorna si se puede robar de la caja
    public static bool IsDrawable(Game game)
    {
        IDrawable drawable = (IDrawable)DependencyContainerRegister.Getter.GetInstance(typeof(IDrawable));

        return drawable.IsDrawable(game);
    }
}