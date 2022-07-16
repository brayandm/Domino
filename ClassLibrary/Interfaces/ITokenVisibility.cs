using System.Diagnostics;

// Esta interfaz representa la visibilidad de un token
public interface ITokenVisibility : IBaseInterface, ISelector
{
    // Esta funcion retorna los jugadores que pueden ver el token
    List<Player> GetTokenVisibilityPlayers(Player individualPlayer, List<Team> teams);
}

// Esta clase representa la clasica visibilidad personal
public class ClassicIndividualTokenVisibility : ITokenVisibility
{
    public List<Player> GetTokenVisibilityPlayers(Player individualPlayer, List<Team> teams)
    {
        return new List<Player>(){individualPlayer};
    }
}

// Esta clase representa la visibilidad de todo el equipo
public class TeamTokenVisibility : ITokenVisibility
{
    public List<Player> GetTokenVisibilityPlayers(Player individualPlayer, List<Team> teams)
    {
        foreach(Team team in teams)
        {
            if(team.Players.Contains(individualPlayer))
            {
                return team.Players;
            }
        }

        return new List<Player>(){individualPlayer};
    }
}

// Esta clase representa la visibilidad de todo el mundo
public class EverybodyTokenVisibility : ITokenVisibility
{
    public List<Player> GetTokenVisibilityPlayers(Player individualPlayer, List<Team> teams)
    {
        List<Player> players = new List<Player>();

        foreach(Team team in teams)
        {
            foreach(Player player in team.Players)
            {
                players.Add(player);
            }
        }

        Debug.Assert(players.Contains(individualPlayer));

        return players;
    }
}