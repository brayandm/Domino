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