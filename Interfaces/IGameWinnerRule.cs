using System.Diagnostics;

interface IGameWinnerRule : IBaseInterface, ISelector
{
    List<Team> GetWinners(Game game);
}

class WinnerRuleMax : IGameWinnerRule
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

class WinnerRuleMin : IGameWinnerRule
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