interface IPlayerGenerator : IBaseInterface
{
    List<Player> GetPlayers();
}

class ClassicFourGreedyPlayers : IPlayerGenerator
{
    public List<Player> GetPlayers()
    {
        List<Player> players = new List<Player>();

        players.Add(new Player("Player1", "Juan", new GreedyStrategy()));
        players.Add(new Player("Player2", "Ana", new GreedyStrategy()));
        players.Add(new Player("Player3", "Pedro", new GreedyStrategy()));
        players.Add(new Player("Player4", "Marta", new GreedyStrategy()));

        return players;
    }
}