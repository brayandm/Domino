public interface IPlayerGenerator : IBaseInterface
{
    Player GetPlayer(string id, string name);
}

class ClassicHumanPlayer : IPlayerGenerator
{
    public Player GetPlayer(string id, string name)
    {
        return new Player(id, name, new HumanSelection());
    }
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

class ClassicFrequencyPlayer : IPlayerGenerator
{
    public Player GetPlayer(string id, string name)
    {
        return new Player(id, name, new FrequencyStrategy());
    }
}

class ClassicTableFrequencyPlayer : IPlayerGenerator
{
    public Player GetPlayer(string id, string name)
    {
        return new Player(id, name, new TableFrequencyStrategy());
    }
}