interface ITeamsGenerator : IBaseInterface
{
    List<Team> GetTeams();
}

class ClassicTwoGreedyTeams : ITeamsGenerator
{
    public List<Team> GetTeams()
    {
        Team teamA = (new ClassicGreedyTeamTwoPlayers()).GetTeam(Names.namer.GetTeamId(), Names.namer.GetTeamName());
        Team teamB = (new ClassicGreedyTeamTwoPlayers()).GetTeam(Names.namer.GetTeamId(), Names.namer.GetTeamName());

        return new List<Team>(){teamA, teamB};
    }
}

class ClassicFourGreedyTeamsWithOnlyOnePlayer : ITeamsGenerator
{
    public List<Team> GetTeams()
    {
        Team teamA = (new ClassicGreedyTeamOnePlayer()).GetTeam(Names.namer.GetTeamId(), Names.namer.GetTeamName());
        Team teamB = (new ClassicGreedyTeamOnePlayer()).GetTeam(Names.namer.GetTeamId(), Names.namer.GetTeamName());
        Team teamC = (new ClassicGreedyTeamOnePlayer()).GetTeam(Names.namer.GetTeamId(), Names.namer.GetTeamName());
        Team teamD = (new ClassicGreedyTeamOnePlayer()).GetTeam(Names.namer.GetTeamId(), Names.namer.GetTeamName());

        return new List<Team>(){teamA, teamB, teamC, teamD};
    }
}

class TwoRandomTeamsWithOnlyOnePlayer : ITeamsGenerator
{
    public List<Team> GetTeams()
    {
        Team teamA = (new ClassicRandomTeamOnePlayer()).GetTeam(Names.namer.GetTeamId(), Names.namer.GetTeamName());
        Team teamB = (new ClassicRandomTeamOnePlayer()).GetTeam(Names.namer.GetTeamId(), Names.namer.GetTeamName());

        return new List<Team>(){teamA, teamB};
    }
}