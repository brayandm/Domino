interface ITeamGenerator : IBaseInterface
{
    Team GetTeam(string id, string name);
}

class ClassicGreedyTeamTwoPlayers : ITeamGenerator
{
    public Team GetTeam(string id, string name)
    {
        List<Player> players = (new TwoGreedyPlayers()).GetPlayers();

        return new Team(id, name, players);
    }
}

class ClassicRandomTeamTwoPlayers : ITeamGenerator
{
    public Team GetTeam(string id, string name)
    {
        List<Player> players = (new TwoRandomPlayers()).GetPlayers();

        return new Team(id, name, players);
    }
}

class ClassicFrequencyTeamTwoPlayers : ITeamGenerator
{
    public Team GetTeam(string id, string name)
    {
        List<Player> players = (new TwoFrequencyPlayers()).GetPlayers();

        return new Team(id, name, players);
    }
}

class ClassicTableFrequencyTeamTwoPlayers : ITeamGenerator
{
    public Team GetTeam(string id, string name)
    {
        List<Player> players = (new TwoTableFrequencyPlayers()).GetPlayers();

        return new Team(id, name, players);
    }
}

class ClassicGreedyTeamOnePlayer : ITeamGenerator
{
    public Team GetTeam(string id, string name)
    {
        List<Player> players = new List<Player>(){(new ClassicGreddyPlayer()).GetPlayer(Names.namer.GetPlayerId(), Names.namer.GetPlayerName())};

        return new Team(id, name, players);
    }
}

class ClassicRandomTeamOnePlayer : ITeamGenerator
{
    public Team GetTeam(string id, string name)
    {
        List<Player> players = new List<Player>(){(new ClassicRandomPlayer()).GetPlayer(Names.namer.GetPlayerId(), Names.namer.GetPlayerName())};

        return new Team(id, name, players);
    }
}