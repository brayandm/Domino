// Esta interfaz representa el calculo de score de un equipo en una ronda
public interface IRoundScoreTeam : IBaseInterface, ISelector
{
    // Esta funcion retorna el puntaje del equipo en la ronda
    int GetScore(Game game, Team team);
}

// Esta clase representa el calculo de puntaje por regla de suma
public class RoundScoreTeamSumRule : IRoundScoreTeam
{
    public int GetScore(Game game, Team team)
    {
        int total = 0;
        
        foreach(Player player in team)
        {
            total += game.GetRoundPlayerScore(player);
        }

        return total;
    }
}

// Esta clase representa el calculo de puntaje por regla de maximo
public class RoundScoreTeamMaxRule : IRoundScoreTeam
{
    public int GetScore(Game game, Team team)
    {
        List<int> scores = new List<int>();

        foreach(Player player in team)
        {
            scores.Add(game.GetRoundPlayerScore(player));
        }

        scores.Sort();

        return scores.Count != 0 ? scores.Last() : 0;
    }
}