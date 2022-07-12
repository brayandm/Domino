interface ITeamGenerator : IBaseInterface
{
    Team GetTeam();
}

class ClassicGreedyTeamTwoPlayers : ITeamGenerator
{
    public Team GetTeam()
    {
        List<Player> players = (new TwoGreedyPlayers()).GetPlayers();

        return new Team("None", players);
    }
}

class ClassicRandomTeamTwoPlayers : ITeamGenerator
{
    public Team GetTeam()
    {
        List<Player> players = (new TwoRandomPlayers()).GetPlayers();

        return new Team("None", players);
    }
}