// Esta interfaz representa la generacion de una lista de players
public interface IPlayersGenerator : IBaseInterface
{
    // Esta funcion retorna una lista de players
    List<Player> GetPlayers();
}

// Esta clase representa cuatro greedy players
public class ClassicFourGreedyPlayers : IPlayersGenerator
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

// Esta clase representa dos random players
public class TwoRandomPlayers : IPlayersGenerator
{
    public List<Player> GetPlayers()
    {
        List<Player> players = new List<Player>();

        players.Add((new ClassicRandomPlayer()).GetPlayer(Names.namer.GetPlayerId(), Names.namer.GetPlayerName()));
        players.Add((new ClassicRandomPlayer()).GetPlayer(Names.namer.GetPlayerId(), Names.namer.GetPlayerName()));

        return players;
    }
}

// Esta clase representa dos greedy players
public class TwoGreedyPlayers : IPlayersGenerator
{
    public List<Player> GetPlayers()
    {
        List<Player> players = new List<Player>();

        players.Add((new ClassicGreddyPlayer()).GetPlayer(Names.namer.GetPlayerId(), Names.namer.GetPlayerName()));
        players.Add((new ClassicGreddyPlayer()).GetPlayer(Names.namer.GetPlayerId(), Names.namer.GetPlayerName()));

        return players;
    }
}

// Esta clase representa dos frequency players
public class TwoFrequencyPlayers : IPlayersGenerator
{
    public List<Player> GetPlayers()
    {
        List<Player> players = new List<Player>();

        players.Add((new ClassicFrequencyPlayer()).GetPlayer(Names.namer.GetPlayerId(), Names.namer.GetPlayerName()));
        players.Add((new ClassicFrequencyPlayer()).GetPlayer(Names.namer.GetPlayerId(), Names.namer.GetPlayerName()));

        return players;
    }
}

// Esta clase representa dos table frequency players
public class TwoTableFrequencyPlayers : IPlayersGenerator
{
    public List<Player> GetPlayers()
    {
        List<Player> players = new List<Player>();

        players.Add((new ClassicTableFrequencyPlayer()).GetPlayer(Names.namer.GetPlayerId(), Names.namer.GetPlayerName()));
        players.Add((new ClassicTableFrequencyPlayer()).GetPlayer(Names.namer.GetPlayerId(), Names.namer.GetPlayerName()));

        return players;
    }
}