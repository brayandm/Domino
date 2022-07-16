using System.Diagnostics;

// Esta clase representa la informacion de los jugadores y equipos
public class TeamInfo
{
    // Esta campo representa los equipos del juego
    public List<Team> Teams;
    // Esta campo representa los jugadores del juego
    public List<Player> Players;
    // Esta campo representa el orden de los turnos en el juego
    public OrderPlayer OrderPlayer;
    // Esta campo representa el tablero de cada jugador
    public Dictionary<Player, Board> PlayerBoard;
    // Esta campo representa el equipo de cada jugador
    public Dictionary<Player, Team> PlayerTeam;

    // Esta funcion chequea cada player este en solo equipo
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

    // Constructor de la clase
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