interface ITeamGenerator : IBaseInterface, ISelector
{
    Tuple<List<Team>, List<Player>> GetTeams();
}

class ClassicTwoGreedyTeams : ITeamGenerator
{
    public Tuple<List<Team>, List<Player>> GetTeams()
    {
        List<Player> players = (new ClassicFourGreedyPlayers()).GetPlayers();

        return new Tuple<List<Team>, List<Player>>(
            new List<Team>(){
                new Team(new List<Player>{players[0], players[2]}),
                new Team(new List<Player>{players[1], players[3]})
                },
            players);
    }
}

class ClassicFourGreedyTeamsWithOnlyOnePlayer : ITeamGenerator
{
    public Tuple<List<Team>, List<Player>> GetTeams()
    {
        List<Player> players = (new ClassicFourGreedyPlayers()).GetPlayers();

        return new Tuple<List<Team>, List<Player>>(
            new List<Team>(){
                new Team(new List<Player>{players[0]}),
                new Team(new List<Player>{players[1]}),
                new Team(new List<Player>{players[2]}),
                new Team(new List<Player>{players[3]}),
            },
            players);
    }
}