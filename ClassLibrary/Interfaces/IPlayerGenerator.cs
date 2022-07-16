// Esta interfaz representa la generacion de un player
public interface IPlayerGenerator : IBaseInterface
{
    // Esta funcion retorna el jugador
    Player GetPlayer(string id, string name);
}

// Esta clase representa un jugador humano
public class ClassicHumanPlayer : IPlayerGenerator
{
    public Player GetPlayer(string id, string name)
    {
        return new Player(id, name, new HumanSelection());
    }
}

// Esta clase representa un jugador greedy
public class ClassicGreddyPlayer : IPlayerGenerator
{
    public Player GetPlayer(string id, string name)
    {
        return new Player(id, name, new GreedyStrategy());
    }
}

// Esta clase representa un jugador random
public class ClassicRandomPlayer : IPlayerGenerator
{
    public Player GetPlayer(string id, string name)
    {
        return new Player(id, name, new RandomStrategy());
    }
}

// Esta clase representa un jugador de frecuencia de tokens del board
public class ClassicFrequencyPlayer : IPlayerGenerator
{
    public Player GetPlayer(string id, string name)
    {
        return new Player(id, name, new FrequencyStrategy());
    }
}

// Esta clase representa un jugador de frecuencia de tokens de la mesa
public class ClassicTableFrequencyPlayer : IPlayerGenerator
{
    public Player GetPlayer(string id, string name)
    {
        return new Player(id, name, new TableFrequencyStrategy());
    }
}