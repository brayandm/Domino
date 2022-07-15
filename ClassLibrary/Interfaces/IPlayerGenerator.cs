public interface IPlayerGenerator : IBaseInterface
{
    Player GetPlayer(string id, string name);
}

public class ClassicHumanPlayer : IPlayerGenerator
{
    public Player GetPlayer(string id, string name)
    {
        return new Player(id, name, new HumanSelection());
    }
}

public class ClassicGreddyPlayer : IPlayerGenerator
{
    public Player GetPlayer(string id, string name)
    {
        return new Player(id, name, new GreedyStrategy());
    }
}

public class ClassicRandomPlayer : IPlayerGenerator
{
    public Player GetPlayer(string id, string name)
    {
        return new Player(id, name, new RandomStrategy());
    }
}

public class ClassicFrequencyPlayer : IPlayerGenerator
{
    public Player GetPlayer(string id, string name)
    {
        return new Player(id, name, new FrequencyStrategy());
    }
}

public class ClassicTableFrequencyPlayer : IPlayerGenerator
{
    public Player GetPlayer(string id, string name)
    {
        return new Player(id, name, new TableFrequencyStrategy());
    }
}