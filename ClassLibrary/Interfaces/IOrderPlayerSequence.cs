public interface IOrderPlayerSequence : IBaseInterface, ISelector
{
    List<Player> GetOrderPlayersequence(List<Player> players);
}

public class ClassicOrderPlayersequence : IOrderPlayerSequence
{
    public List<Player> GetOrderPlayersequence(List<Player> players)
    {
        return new List<Player>(players);
    }
}

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