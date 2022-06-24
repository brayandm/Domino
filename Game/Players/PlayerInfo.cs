class PlayerInfo
{
    public List<Player> Players;

    public IOrderPlayer OrderPlayer;

    public PlayerInfo(List<Player> players, IOrderPlayer orderPlayer)
    {
        this.Players = players;
        this.OrderPlayer = orderPlayer;
    }
}