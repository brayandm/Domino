class Team
{
    public string Id;
    public string Name;
    public List<Player> Players = new List<Player>();

    public int Count { get { return this.Players.Count; } }

    public Team(string id, string name, List<Player> players)
    {
        this.Id = id;
        this.Name = name;
        this.Players = players;
    }

    public void Add(Player player) 
    {
        if(!this.Players.Contains(player))
        {
            Players.Add(player);
        }
    }

    public void Remove(Player player) 
    {
        if(this.Players.Contains(player))
        {
            Players.Remove(player);
        }
    }

    public IEnumerator<Player> GetEnumerator()
    {
        foreach(Player player in this.Players)
        {
            yield return player;
        }
    }

    public bool Contains(Player player)
    {
        return this.Players.Contains(player);
    }
}