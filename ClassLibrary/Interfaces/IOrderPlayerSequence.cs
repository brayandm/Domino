// Esta interfaz representa el orden de los turnos
public interface IOrderPlayerSequence : IBaseInterface, ISelector
{
    // Esta funcion retorna una secuencia de turnos
    List<Player> GetOrderPlayersequence(List<Player> players);
}

// Esta clase representa un clasico orden
public class ClassicOrderPlayersequence : IOrderPlayerSequence
{
    public List<Player> GetOrderPlayersequence(List<Player> players)
    {
        return new List<Player>(players);
    }
}

// Esta clase representa dos turnos seguidos con el mismo jugador
public class TwoTurnsOrderPlayersequence : IOrderPlayerSequence
{
    public List<Player> GetOrderPlayersequence(List<Player> players)
    {
        List<Player> sequence = new List<Player>();

        foreach(Player player in players)
        {
            sequence.Add(player);
            sequence.Add(player);
        }

        return sequence;
    }
}