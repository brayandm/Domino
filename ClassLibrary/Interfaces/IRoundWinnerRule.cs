using System.Diagnostics;

// Esta interfaz representa la regla de ganadores de una ronda
public interface IRoundWinnerRule : IBaseInterface, ISelector
{
    // Esta funcion retorna los ganadores de una ronda del juego
    List<Team> GetWinners(Game game);
}

// Esta clase representa el ganadores por regla de maximo
public class RoundWinnerRuleMax : IRoundWinnerRule
{
    public List<Team> GetWinners(Game game)
    {
        List<Tuple<Team, int>> teamScores = new List<Tuple<Team, int>>();
        
        foreach(Team team in game.GetAllTeams())
        {
            teamScores.Add(new Tuple<Team, int>(team, game.GetRoundTeamScore(team)));
        }
        
        Debug.Assert(teamScores.Count > 0);

        teamScores.Sort((x, y) => x.Item2.CompareTo(y.Item2));

        int maxScore = teamScores.Last().Item2;

        List<Team> winners = new List<Team>();

        foreach(Tuple<Team, int> teamScore in teamScores)
        {
            if(teamScore.Item2 == maxScore)
            {
                winners.Add(teamScore.Item1);
            }
        }

        return winners;
    }
}

// Esta clase representa el ganadores por regla de minimo
public class RoundWinnerRuleMin : IRoundWinnerRule
{
    public List<Team> GetWinners(Game game)
    {
        List<Tuple<Team, int>> teamScores = new List<Tuple<Team, int>>();
        
        foreach(Team team in game.GetAllTeams())
        {
            teamScores.Add(new Tuple<Team, int>(team, game.GetRoundTeamScore(team)));
        }
        
        Debug.Assert(teamScores.Count > 0);

        teamScores.Sort((x, y) => x.Item2.CompareTo(y.Item2));
        teamScores.Reverse();

        int maxScore = teamScores.Last().Item2;

        List<Team> winners = new List<Team>();

        foreach(Tuple<Team, int> teamScore in teamScores)
        {
            if(teamScore.Item2 == maxScore)
            {
                winners.Add(teamScore.Item1);
            }
        }

        return winners;
    }
}