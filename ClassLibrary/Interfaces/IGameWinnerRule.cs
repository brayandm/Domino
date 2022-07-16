using System.Diagnostics;

// Esta interfaz representa la regla del ganador del juego
public interface IGameWinnerRule : IBaseInterface, ISelector
{
    // Esta funcion obtiene los ganadores del juego
    List<Team> GetWinners(Game game);
}

// Esta clase representa la regla del maximo score
public class WinnerRuleMax : IGameWinnerRule
{
    public List<Team> GetWinners(Game game)
    {
        List<Tuple<Team, int>> teamScores = new List<Tuple<Team, int>>();
        
        foreach(Team team in game.GetAllTeams())
        {
            teamScores.Add(new Tuple<Team, int>(team, game.GetTeamAllRoundScore(team)));
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

// Esta clase representa la regla del minimo score
public class WinnerRuleMin : IGameWinnerRule
{
    public List<Team> GetWinners(Game game)
    {
        List<Tuple<Team, int>> teamScores = new List<Tuple<Team, int>>();
        
        foreach(Team team in game.GetAllTeams())
        {
            teamScores.Add(new Tuple<Team, int>(team, game.GetTeamAllRoundScore(team)));
        }
        
        Debug.Assert(teamScores.Count > 0);

        teamScores.Sort((x, y) => x.Item2.CompareTo(y.Item2));
        teamScores.Reverse();

        int minScore = teamScores.Last().Item2;

        List<Team> winners = new List<Team>();

        foreach(Tuple<Team, int> teamScore in teamScores)
        {
            if(teamScore.Item2 == minScore)
            {
                winners.Add(teamScore.Item1);
            }
        }

        return winners;
    }
}

// Esta clase representa la regla del minimo score y un solo ganador
public class OnlyOneWinnerRuleMin : IGameWinnerRule
{
    public List<Team> GetWinners(Game game)
    {
        List<Tuple<Team, int>> teamScores = new List<Tuple<Team, int>>();
        
        foreach(Team team in game.GetAllTeams())
        {
            teamScores.Add(new Tuple<Team, int>(team, game.GetTeamAllRoundScore(team)));
        }
        
        Debug.Assert(teamScores.Count > 0);

        teamScores.Sort((x, y) => x.Item2.CompareTo(y.Item2));
        teamScores.Reverse();

        return new List<Team>(){teamScores.Last().Item1};
    }
}