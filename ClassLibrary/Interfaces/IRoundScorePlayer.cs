// Esta interfaz representa el score de un jugador en una ronda
public interface IRoundScorePlayer : IBaseInterface, ISelector
{
    // Esta funcion retorna el score de un jugador en la ronda
    int GetScore(Game game, Player player);
}

// Esta clase representa el calculo de puntaje por regla de suma
public class RoundScorePlayerSumRule : IRoundScorePlayer
{
    public int GetScore(Game game, Player player) 
    {
        int score = 0;

        foreach(ProtectedToken token in game.GetPlayerBoard(player).GetTokens())
        {
            score += token.GetTokenWithoutVisibility().GetValue();
        }

        return score;
    }
}

// Esta clase representa el calculo de puntaje por regla de maximo
public class RoundScorePlayerMaxRule : IRoundScorePlayer
{
    public int GetScore(Game game, Player player)
    {
        List<int> totalValues = new List<int>();

        foreach(ProtectedToken token in game.GetPlayerBoard(player).GetTokens())
        {
            totalValues.Add(token.GetTokenWithoutVisibility().GetValue());
        }

        totalValues.Sort();

        return totalValues.Count != 0 ? totalValues.Last() : 0;
    }
}

// Esta clase representa el calculo de puntaje por regla de cantidad de turnos pasados
public class RoundPassesScorePlayer : IRoundScorePlayer
{
    public int GetScore(Game game, Player player) 
    {
        return game.GetPlayerTotalPassedTurns(player);
    }
}