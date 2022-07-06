using System.Diagnostics;

class PlayerInfo
{
    public List<Player> Players;
    public IOrderPlayer OrderPlayer;
    public Dictionary<Player, Board> PlayerBoard;

    public PlayerInfo(List<Player> players, List<Board> boards)
    {
        Debug.Assert(players.Count == boards.Count);

        this.Players = players;
        this.OrderPlayer = (IOrderPlayer)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(IOrderPlayer));
        this.PlayerBoard = new Dictionary<Player, Board>();

        this.OrderPlayer.Init(players);

        for(int i = 0 ; i < Math.Min(players.Count, boards.Count) ; i++)
        {
            PlayerBoard.Add(players[i], boards[i]);
        }
    }
}