using System.Diagnostics;

public class TeamInfo
{
    public List<Team> Teams;
    public List<Player> Players;
    public OrderPlayer OrderPlayer;
    public Dictionary<Player, Board> PlayerBoard;
    public Dictionary<Player, Team> PlayerTeam;

    private void CheckTeamsAndPlayers(List<Team> teams, List<Player> players)
    {
        foreach(Team team in teams)
        {   
            foreach(Player player in team.Players)
            {
                Debug.Assert(players.Contains(player));
            }
        }

        foreach(Player player in players)
        {
            int numberOfTeamsThatBelongs = 0;

            foreach(Team team in teams)
            {
                if(team.Players.Contains(player))
                {
                    numberOfTeamsThatBelongs++;
                }
            }

            Debug.Assert(numberOfTeamsThatBelongs == 1);
        }
    }

    public TeamInfo(List<Team> teams, List<Player> players, List<Board> boards, IOrderPlayerSequence orderPlayerSequence)
    {
        Debug.Assert(players.Count == boards.Count);

        this.CheckTeamsAndPlayers(teams, players);

        this.Teams = teams;
        this.Players = players;
        this.OrderPlayer = new OrderPlayer(players, orderPlayerSequence);
        this.PlayerBoard = new Dictionary<Player, Board>();
        this.PlayerTeam = new Dictionary<Player, Team>();

        for(int i = 0 ; i < Math.Min(players.Count, boards.Count) ; i++)
        {
            PlayerBoard.Add(players[i], boards[i]);
        }

        foreach(Team team in teams)
        {
            foreach(Player player in team.Players)
            {
                this.PlayerTeam.Add(player, team);
            }
        }
    }
}