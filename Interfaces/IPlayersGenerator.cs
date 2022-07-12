interface IPlayersGenerator : IBaseInterface
{
    List<Player> GetPlayers();
}

class ClassicFourGreedyPlayers : IPlayersGenerator
{
    public List<Player> GetPlayers()
    {
        List<Player> players = new List<Player>();

        players.Add((new ClassicGreddyPlayer()).GetPlayer("Player1", "Juan"));
        players.Add((new ClassicGreddyPlayer()).GetPlayer("Player2", "Ana"));
        players.Add((new ClassicGreddyPlayer()).GetPlayer("Player3", "Pedro"));
        players.Add((new ClassicGreddyPlayer()).GetPlayer("Player4", "Marta"));

        return players;
    }
}

class TwoRandomPlayers : IPlayersGenerator
{
    public List<Player> GetPlayers()
    {
        List<Player> players = new List<Player>();

        players.Add((new ClassicRandomPlayer()).GetPlayer("Player1", "Carlos"));
        players.Add((new ClassicRandomPlayer()).GetPlayer("Player2", "Maria"));

        return players;
    }
}

class TwoGreedyPlayers : IPlayersGenerator
{
    public List<Player> GetPlayers()
    {
        List<Player> players = new List<Player>();

        players.Add((new ClassicGreddyPlayer()).GetPlayer("Player1", "Carlos"));
        players.Add((new ClassicGreddyPlayer()).GetPlayer("Player2", "Maria"));

        return players;
    }
}