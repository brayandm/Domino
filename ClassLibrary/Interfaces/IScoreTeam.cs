// Esta interfaz representa el score de un equipo en el juego
public interface IScoreTeam : IBaseInterface, ISelector
{
    // Esta funcion retorna el score de un equipo en el juego
    int GetScore(Game game, Team team);
}

// Esta clase representa el calculo de puntaje de un equipo en el juego
//usando regla de la suma
public class ScoreTeamSumRule : IScoreTeam
{
    public int GetScore(Game game, Team team)
    {
        List<int> scores = game.GetTeamAllRoundScores(team);

        int total = 0;
        
        foreach(int score in scores)
        {
            total += score;
        }

        return total;
    }
}

// Esta clase representa el calculo de puntaje de un equipo en el juego
//usando regla del maximo

public class ScoreTeamMaxRule : IScoreTeam
{
    public int GetScore(Game game, Team team)
    {
        List<int> scores = game.GetTeamAllRoundScores(team);

        scores.Sort();

        return scores.Count != 0 ? scores.Last() : 0;
    }
}