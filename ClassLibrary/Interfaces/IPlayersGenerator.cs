public interface IPlayersGenerator : IBaseInterface
{
    List<Player> GetPlayers();
}

class ClassicFourGreedyPlayers : IPlayersGenerator
{
    public List<Player> GetPlayers()
    {
        List<Player> players = new List<Player>();

        players.Add((new ClassicGreddyPlayer()).GetPlayer(Names.namer.GetPlayerId(), Names.namer.GetPlayerName()));
        players.Add((new ClassicGreddyPlayer()).GetPlayer(Names.namer.GetPlayerId(), Names.namer.GetPlayerName()));
        players.Add((new ClassicGreddyPlayer()).GetPlayer(Names.namer.GetPlayerId(), Names.namer.GetPlayerName()));
        players.Add((new ClassicGreddyPlayer()).GetPlayer(Names.namer.GetPlayerId(), Names.namer.GetPlayerName()));

        return players;
    }
}

class TwoRandomPlayers : IPlayersGenerator
{
    public List<Player> GetPlayers()
    {
        List<Player> players = new List<Player>();

        players.Add((new ClassicRandomPlayer()).GetPlayer(Names.namer.GetPlayerId(), Names.namer.GetPlayerName()));
        players.Add((new ClassicRandomPlayer()).GetPlayer(Names.namer.GetPlayerId(), Names.namer.GetPlayerName()));

        return players;
    }
}

class TwoGreedyPlayers : IPlayersGenerator
{
    public List<Player> GetPlayers()
    {
        List<Player> players = new List<Player>();

        players.Add((new ClassicGreddyPlayer()).GetPlayer(Names.namer.GetPlayerId(), Names.namer.GetPlayerName()));
        players.Add((new ClassicGreddyPlayer()).GetPlayer(Names.namer.GetPlayerId(), Names.namer.GetPlayerName()));

        return players;
    }
}

class TwoFrequencyPlayers : IPlayersGenerator
{
    public List<Player> GetPlayers()
    {
        List<Player> players = new List<Player>();

        players.Add((new ClassicFrequencyPlayer()).GetPlayer(Names.namer.GetPlayerId(), Names.namer.GetPlayerName()));
        players.Add((new ClassicFrequencyPlayer()).GetPlayer(Names.namer.GetPlayerId(), Names.namer.GetPlayerName()));

        return players;
    }
}

class TwoTableFrequencyPlayers : IPlayersGenerator
{
    public List<Player> GetPlayers()
    {
        List<Player> players = new List<Player>();

        players.Add((new ClassicTableFrequencyPlayer()).GetPlayer(Names.namer.GetPlayerId(), Names.namer.GetPlayerName()));
        players.Add((new ClassicTableFrequencyPlayer()).GetPlayer(Names.namer.GetPlayerId(), Names.namer.GetPlayerName()));

        return players;
    }
}