interface ITeamsGenerator : IBaseInterface
{
    List<Team> GetTeams();
}

class ClassicTwoGreedyTeams : ITeamsGenerator
{
    public List<Team> GetTeams()
    {
        Team teamA = (new ClassicGreedyTeamTwoPlayers()).GetTeam("Team1", "Team A");
        Team teamB = (new ClassicGreedyTeamTwoPlayers()).GetTeam("Team2", "Team B");

        return new List<Team>(){teamA, teamB};
    }
}

class ClassicFourGreedyTeamsWithOnlyOnePlayer : ITeamsGenerator
{
    public List<Team> GetTeams()
    {
        Team teamA = (new ClassicGreedyTeamOnePlayer()).GetTeam("Team1", "Team A");
        Team teamB = (new ClassicGreedyTeamOnePlayer()).GetTeam("Team2", "Team B");
        Team teamC = (new ClassicGreedyTeamOnePlayer()).GetTeam("Team3", "Team C");
        Team teamD = (new ClassicGreedyTeamOnePlayer()).GetTeam("Team4", "Team D");

        return new List<Team>(){teamA, teamB, teamC, teamD};
    }
}

class TwoRandomTeamsWithOnlyOnePlayer : ITeamsGenerator
{
    public List<Team> GetTeams()
    {
        Team teamA = (new ClassicRandomTeamOnePlayer()).GetTeam("Team1", "Team A");
        Team teamB = (new ClassicRandomTeamOnePlayer()).GetTeam("Team2", "Team B");

        return new List<Team>(){teamA, teamB};
    }
}