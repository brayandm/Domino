using System.Diagnostics;

interface ITournamentGenerator : IBaseInterface, ISelector
{
    Tournament GetTournament();
}

class GetTournamentOneClassicMatch : ITournamentGenerator
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

class GetEliminationTournamentFourTeams : ITournamentGenerator
{
    public Tournament GetTournament()
    {
        List<Team> teamsA = (new ClassicTwoGreedyTeams()).GetTeams();
        List<Team> teamsB = (new ClassicTwoGreedyTeams()).GetTeams();

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