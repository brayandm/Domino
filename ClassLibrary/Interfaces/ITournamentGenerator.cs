using System.Diagnostics;

// Esta interfaz representa la generacion de un torneo
public interface ITournamentGenerator : IBaseInterface, ISelector
{
    // Esta funcion retorna un torneo
    Tournament GetTournament();
}

// Esta clase representa un torneo de solo un partido con dos equipos greedies
public class GetTournamentOneClassicMatch : ITournamentGenerator
{
    public Tournament GetTournament()
    {
        List<Team> teams = (new ClassicTwoGreedyTeams()).GetTeams();

        Match principal = new Match();

        principal.AddTeams(teams);

        List<Match> matches = new List<Match>(){principal};

        Dictionary<Match, List<Match>> Graph = new Dictionary<Match, List<Match>>();

        return new Tournament(matches, Graph);
    }
}

// Esta clase representa un torneo de solo un partido con 3 equipoes greedies y uno humano
public class GetTournamentOneMatchThreeGreedyAndHumanPlayers : ITournamentGenerator
{
    public Tournament GetTournament()
    {
        List<Team> teams = (new ClassicThreeGreedyAndHumanTeamsWithOnlyOnePlayer()).GetTeams();

        Match principal = new Match();

        principal.AddTeams(teams);

        List<Match> matches = new List<Match>(){principal};

        Dictionary<Match, List<Match>> Graph = new Dictionary<Match, List<Match>>();

        return new Tournament(matches, Graph);
    }
}

// Esta clase representa un torneo de solo un partido con 3 equipoes random y uno humano
public class GetTournamentOneMatchRandomAndHumanPlayers : ITournamentGenerator
{
    public Tournament GetTournament()
    {
        List<Team> teams = (new ClassicRandomAndHumanTeamsWithOnlyOnePlayer()).GetTeams();

        Match principal = new Match();

        principal.AddTeams(teams);

        List<Match> matches = new List<Match>(){principal};

        Dictionary<Match, List<Match>> Graph = new Dictionary<Match, List<Match>>();

        return new Tournament(matches, Graph);
    }
}

// Esta clase representa un torneo de eliminacion de 4 equipos
public class GetEliminationTournamentFourTeams : ITournamentGenerator
{
    public Tournament GetTournament()
    {
        List<Team> teamsA = (new GreedyAndRandomTeams()).GetTeams();
        List<Team> teamsB = (new FrequencyAndTableFrequencyTeams()).GetTeams();

        Match semifinalA = new Match();
        Match semifinalB = new Match();
        Match final = new Match();

        semifinalA.AddTeams(teamsA);
        semifinalB.AddTeams(teamsB);
        
        Dictionary<Match, List<Match>> graph = new Dictionary<Match, List<Match>>();

        graph.Add(semifinalA, new List<Match>(){final});
        graph.Add(semifinalB, new List<Match>(){final});

        List<Match> matches = new List<Match>(){semifinalA, semifinalB, final};

        return new Tournament(matches, graph);
    }
}