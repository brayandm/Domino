class Team
{
    public List<Player> Players = new List<Player>();

    public int Count { get { return this.Players.Count; } }

    public Team(List<Player> players)
    {
        this.Players = players;
    }
}