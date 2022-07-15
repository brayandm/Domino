using System.Diagnostics;

public interface ITournamentWinnerRule : IBaseInterface, ISelector
{
    List<Team> GetTournamentWinners(Tournament tournament);
}

class ClassicTournamentMaxGamesWon : ITournamentWinnerRule
{
    public List<Team> GetTournamentWinners(Tournament tournament)
    {
        List<Team> teams = tournament.GetTeams();

        List<Tuple<Team, int>> teamScores = new List<Tuple<Team, int>>();
        
        foreach(Team team in teams)
        {
            teamScores.Add(new Tuple<Team, int>(team, tournament.GetTournamentHistory().GetTeamMatchesWon(team)));
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

class ClassicTournamentMinScore : ITournamentWinnerRule
{
    public List<Team> GetTournamentWinners(Tournament tournament)
    {
        List<Team> teams = tournament.GetTeams();

        List<Tuple<Team, int>> teamScores = new List<Tuple<Team, int>>();
        
        foreach(Team team in teams)
        {
            teamScores.Add(new Tuple<Team, int>(team, tournament.GetTournamentHistory().GetTeamTotalScore(team)));
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