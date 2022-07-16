// Esta interfaz representa la regla de finalizacion de una ronda
public interface IRoundFinalizationRule : IBaseInterface, ISelector
{
    // Esta funcion indica si se cumple la condicion
    bool IsRoundGameOver(Game game);
}

// Esta clase representa la clasica finalizacion donde se pasan todos o un jugador
//lo juega todo
public class ClassicFinalizationRule : IRoundFinalizationRule 
{
    public bool IsRoundGameOver(Game game) 
    {
        return game.CurrentPlayerPlayedAllTokens() || game.GetNumberOfContiguousPassedTurns() == game.GetNumberOfPlayers();
    }
}

// Esta clase representa la clasica finalizacion donde termina si se juega un doble
public class DoubleFinalizationRule : IRoundFinalizationRule 
{
    public bool IsRoundGameOver(Game game) 
    {
        if(game.CurrentPlayerPlayedAllTokens())
        {
            return true;
        }

        Move? move = game.GetLastMove();

        if(move != null && move.Token != null && move.Token.GetTokenWithoutVisibility().IsDouble()) 
        {
            return true;
        }

        return false;
    }
}












