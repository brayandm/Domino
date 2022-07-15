public interface ITeamOrder : IBaseInterface, ISelector
{
    List<Player> GetTeamOrder(List<Team> teams);
}

public class AlternateTeamOrder : ITeamOrder
{
    public List<Player> GetTeamOrder(List<Team> teams)
    {
        List<Player> players = new List<Player>();

        List<List<Player>> temporalTeams = new List<List<Player>>();

        foreach(Team team in teams)
        {
            List<Player> temporalPlayers = new List<Player>();
            
            foreach(Player player in team.Players)
            {
                temporalPlayers.Add(player);
            }

            temporalTeams.Add(temporalPlayers);
        }

        while(true)
        {
            bool doNothing = true;

            foreach(List<Player> team in temporalTeams)
            {
                if(team.Count > 0)
                {
                    players.Add(team.Last());
                    team.RemoveAt(team.Count - 1);
                    doNothing = false;
                }
            }

            if(doNothing)
            {
                break;
            }
        }

        return players;
    }
}

public class adjacentTeamOrder : ITeamOrder
{
    public List<Player> GetTeamOrder(List<Team> teams)
    {
        List<Player> players = new List<Player>();

        foreach(Team team in teams)
        {
            foreach(Player player in team.Players)
            {
                players.Add(player);
            }
        }

        return players;
    }
}