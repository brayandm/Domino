interface ITeamGenerator : IBaseInterface, ISelector
{
    List<Team> GetTeams();
}

class ClassicTwoGreedyTeams : ITeamGenerator
{
    public List<Team> GetTeams()
    {
        List<Player> players = (new ClassicFourGreedyPlayers()).GetPlayers();

        return new List<Team>(){
            new Team(new List<Player>{players[0], players[2]}),
            new Team(new List<Player>{players[1], players[3]})
        };
    }
}

class ClassicFourGreedyTeamsWithOnlyOnePlayer : ITeamGenerator
{
    public List<Team> GetTeams()
    {
        List<Player> players = (new ClassicFourGreedyPlayers()).GetPlayers();

        return new List<Team>(){
            new Team(new List<Player>{players[0]}),
            new Team(new List<Player>{players[1]}),
            new Team(new List<Player>{players[2]}),
            new Team(new List<Player>{players[3]}),
        };
    }
}