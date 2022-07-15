using System.Diagnostics;

public interface ITokenVisibility : IBaseInterface, ISelector
{
    List<Player> GetTokenVisibilityPlayers(Player individualPlayer, List<Team> teams);
}

public class ClassicIndividualTokenVisibility : ITokenVisibility
{
    public List<Player> GetTokenVisibilityPlayers(Player individualPlayer, List<Team> teams)
    {
        return new List<Player>(){individualPlayer};
    }
}

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