// Esta interfaz representa la generacion de una lista de equipos
public interface ITeamsGenerator : IBaseInterface
{
    // Esta funcion retorna la lista de equipos
    List<Team> GetTeams();
}

// Esta clase representa la generacion de dos equipos greedies
public class ClassicTwoGreedyTeams : ITeamsGenerator
{
    public List<Team> GetTeams()
    {
        Team teamA = (new ClassicGreedyTeamTwoPlayers()).GetTeam(Names.namer.GetTeamId(), Names.namer.GetTeamName());
        Team teamB = (new ClassicGreedyTeamTwoPlayers()).GetTeam(Names.namer.GetTeamId(), Names.namer.GetTeamName());

        return new List<Team>(){teamA, teamB};
    }
}

// Esta clase representa la generacion de un equipo greedy y otro random
public class GreedyAndRandomTeams : ITeamsGenerator
{
    public List<Team> GetTeams()
    {
        Team teamA = (new ClassicGreedyTeamTwoPlayers()).GetTeam(Names.namer.GetTeamId(), Names.namer.GetTeamName());
        Team teamB = (new ClassicRandomTeamTwoPlayers()).GetTeam(Names.namer.GetTeamId(), Names.namer.GetTeamName());

        return new List<Team>(){teamA, teamB};
    }
}

// Esta clase representa la generacion de un equipo de frecuencia y otro de frecuencia de mesa
public class FrequencyAndTableFrequencyTeams : ITeamsGenerator
{
    public List<Team> GetTeams()
    {
        Team teamA = (new ClassicFrequencyTeamTwoPlayers()).GetTeam(Names.namer.GetTeamId(), Names.namer.GetTeamName());
        Team teamB = (new ClassicTableFrequencyTeamTwoPlayers()).GetTeam(Names.namer.GetTeamId(), Names.namer.GetTeamName());

        return new List<Team>(){teamA, teamB};
    }
}

// Esta clase representa la generacion de 4 equipos greedies de un solo jugador
public class ClassicFourGreedyTeamsWithOnlyOnePlayer : ITeamsGenerator
{
    public List<Team> GetTeams()
    {
        Team teamA = (new ClassicGreedyTeamOnePlayer()).GetTeam(Names.namer.GetTeamId(), Names.namer.GetTeamName());
        Team teamB = (new ClassicGreedyTeamOnePlayer()).GetTeam(Names.namer.GetTeamId(), Names.namer.GetTeamName());
        Team teamC = (new ClassicGreedyTeamOnePlayer()).GetTeam(Names.namer.GetTeamId(), Names.namer.GetTeamName());
        Team teamD = (new ClassicGreedyTeamOnePlayer()).GetTeam(Names.namer.GetTeamId(), Names.namer.GetTeamName());

        return new List<Team>(){teamA, teamB, teamC, teamD};
    }
}

// Esta clase representa la generacion de 2 equipos randoms de un jugador
public class TwoRandomTeamsWithOnlyOnePlayer : ITeamsGenerator
{
    public List<Team> GetTeams()
    {
        Team teamA = (new ClassicRandomTeamOnePlayer()).GetTeam(Names.namer.GetTeamId(), Names.namer.GetTeamName());
        Team teamB = (new ClassicRandomTeamOnePlayer()).GetTeam(Names.namer.GetTeamId(), Names.namer.GetTeamName());

        return new List<Team>(){teamA, teamB};
    }
}

// Esta clase representa la generacion de un equipo random y otro humano de 1 jugador ambos
public class ClassicRandomAndHumanTeamsWithOnlyOnePlayer : ITeamsGenerator
{
    public List<Team> GetTeams()
    {
        Team teamA = (new ClassicRandomTeamOnePlayer()).GetTeam(Names.namer.GetTeamId(), Names.namer.GetTeamName());
        Team teamB = (new ClassicHumanTeamOnePlayer()).GetTeam(Names.namer.GetTeamId(), Names.namer.GetTeamName());

        return new List<Team>(){teamA, teamB};
    }
}

// Esta clase representa la generacion de 3 equipos greedies y otro humano de 1 jugador todos
public class ClassicThreeGreedyAndHumanTeamsWithOnlyOnePlayer : ITeamsGenerator
{
    public List<Team> GetTeams()
    {
        Team teamA = (new ClassicGreedyTeamOnePlayer()).GetTeam(Names.namer.GetTeamId(), Names.namer.GetTeamName());
        Team teamB = (new ClassicGreedyTeamOnePlayer()).GetTeam(Names.namer.GetTeamId(), Names.namer.GetTeamName());
        Team teamC = (new ClassicGreedyTeamOnePlayer()).GetTeam(Names.namer.GetTeamId(), Names.namer.GetTeamName());
        Team teamD = (new ClassicHumanTeamOnePlayer()).GetTeam(Names.namer.GetTeamId(), Names.namer.GetTeamName());

        return new List<Team>(){teamA, teamB, teamC, teamD};
    }
}