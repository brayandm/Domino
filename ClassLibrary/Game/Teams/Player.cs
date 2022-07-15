public class Player
{
    public string Id;

    public string Name;

    public IStrategy Strategy;

    public Player(string id, string name, IStrategy strategy)
    {
        this.Id = id;
        this.Name = name;
        this.Strategy = strategy;
    }
}