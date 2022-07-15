public interface ITeamGenerator : IBaseInterface
{
    Team GetTeam(string id, string name);
}

public class ClassicGreedyTeamTwoPlayers : ITeamGenerator
{
    public Team GetTeam(string id, string name)
    {
        List<Player> players = (new TwoGreedyPlayers()).GetPlayers();

        return new Team(id, name, players);
    }
}

public class ClassicRandomTeamTwoPlayers : ITeamGenerator
{
    public Team GetTeam(string id, string name)
    {
        List<Player> players = (new TwoRandomPlayers()).GetPlayers();

        return new Team(id, name, players);
    }
}

public class ClassicFrequencyTeamTwoPlayers : ITeamGenerator
{
    public Team GetTeam(string id, string name)
    {
        List<Player> players = (new TwoFrequencyPlayers()).GetPlayers();

        return new Team(id, name, players);
    }
}

public class ClassicTableFrequencyTeamTwoPlayers : ITeamGenerator
{
    public Team GetTeam(string id, string name)
    {
        List<Player> players = (new TwoTableFrequencyPlayers()).GetPlayers();

        return new Team(id, name, players);
    }
}

public class ClassicGreedyTeamOnePlayer : ITeamGenerator
{
    public Team GetTeam(string id, string name)
    {
        List<Player> players = new List<Player>(){(new ClassicGreddyPlayer()).GetPlayer(Names.namer.GetPlayerId(), Names.namer.GetPlayerName())};

        return new Team(id, name, players);
    }
}

public class ClassicRandomTeamOnePlayer : ITeamGenerator
{
    public Team GetTeam(string id, string name)
    {
        List<Player> players = new List<Player>(){(new ClassicRandomPlayer()).GetPlayer(Names.namer.GetPlayerId(), Names.namer.GetPlayerName())};

        return new Team(id, name, players);
    }
}

public class ClassicHumanTeamOnePlayer : ITeamGenerator
{
    public Team GetTeam(string id, string name)
    {
        List<Player> players = new List<Player>(){(new ClassicHumanPlayer()).GetPlayer(Names.namer.GetPlayerId(), Names.namer.GetHumanName())};

        return new Team(id, name, players);
    }
}