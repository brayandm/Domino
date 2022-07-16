// Esta interfaz representa la generacion de un equipo
public interface ITeamGenerator : IBaseInterface
{
    // Esta funcion retorna un equipo
    Team GetTeam(string id, string name);
}

// ESta clase representa la generacion de un equipo de 2 jugadores greedies
public class ClassicGreedyTeamTwoPlayers : ITeamGenerator
{
    public Team GetTeam(string id, string name)
    {
        List<Player> players = (new TwoGreedyPlayers()).GetPlayers();

        return new Team(id, name, players);
    }
}

// ESta clase representa la generacion de un equipo de 2 jugadores randoms
public class ClassicRandomTeamTwoPlayers : ITeamGenerator
{
    public Team GetTeam(string id, string name)
    {
        List<Player> players = (new TwoRandomPlayers()).GetPlayers();

        return new Team(id, name, players);
    }
}

// ESta clase representa la generacion de un equipo de 2 jugadores de frecuencias
public class ClassicFrequencyTeamTwoPlayers : ITeamGenerator
{
    public Team GetTeam(string id, string name)
    {
        List<Player> players = (new TwoFrequencyPlayers()).GetPlayers();

        return new Team(id, name, players);
    }
}

// ESta clase representa la generacion de un equipo de 2 jugadores de frecuencias de la mesa
public class ClassicTableFrequencyTeamTwoPlayers : ITeamGenerator
{
    public Team GetTeam(string id, string name)
    {
        List<Player> players = (new TwoTableFrequencyPlayers()).GetPlayers();

        return new Team(id, name, players);
    }
}

// ESta clase representa la generacion de un equipo de 1 jugador greedy
public class ClassicGreedyTeamOnePlayer : ITeamGenerator
{
    public Team GetTeam(string id, string name)
    {
        List<Player> players = new List<Player>(){(new ClassicGreddyPlayer()).GetPlayer(Names.namer.GetPlayerId(), Names.namer.GetPlayerName())};

        return new Team(id, name, players);
    }
}

// ESta clase representa la generacion de un equipo de 1 jugador random
public class ClassicRandomTeamOnePlayer : ITeamGenerator
{
    public Team GetTeam(string id, string name)
    {
        List<Player> players = new List<Player>(){(new ClassicRandomPlayer()).GetPlayer(Names.namer.GetPlayerId(), Names.namer.GetPlayerName())};

        return new Team(id, name, players);
    }
}

// ESta clase representa la generacion de un equipo de 1 jugador humano
public class ClassicHumanTeamOnePlayer : ITeamGenerator
{
    public Team GetTeam(string id, string name)
    {
        List<Player> players = new List<Player>(){(new ClassicHumanPlayer()).GetPlayer(Names.namer.GetPlayerId(), Names.namer.GetHumanName())};

        return new Team(id, name, players);
    }
}