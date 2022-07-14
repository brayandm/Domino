interface IPlayerGenerator : IBaseInterface
{
    Player GetPlayer(string id, string name);
}

class ClassicGreddyPlayer : IPlayerGenerator
{
    public Player GetPlayer(string id, string name)
    {
        return new Player(id, name, new GreedyStrategy());
    }
}

class ClassicRandomPlayer : IPlayerGenerator
{
    public Player GetPlayer(string id, string name)
    {
        return new Player(id, name, new RandomStrategy());
    }
}