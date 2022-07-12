using System.Diagnostics;

class Match
{
    private Game? _game;

    private List<Team> _teams = new List<Team>();

    public List<Team> GetTeams()
    {
        return this._teams;
    }

    public void AddTeam(Team team)
    {
        this._teams.Add(team);
    }

    public void RemoveTeam(Team team)
    {
        this._teams.Remove(team);
    }

    public void AddTeams(List<Team> teams)
    {
        foreach(Team team in teams)
        {
            this.AddTeam(team);
        }
    }

    public void RemoveTeams(List<Team> teams)
    {
        foreach(Team team in teams)
        {
            this.RemoveTeam(team);
        }
    }

    public void PlayMatch()
    {
        Debug.Assert(this._teams.Count > 0);

        Graphics.graphicinterface.NewGame();

        this._game = new Game(this._teams);

        Events.MainEvent mainEvent = new Events.MainEvent();

        mainEvent.Start(this._game, Graphics.graphicinterface);

        Graphics.graphicinterface.GameOver(this._game);
    }

    public List<Team> GetWinners()
    {
        if(this._game is not Game)
        {
            this.PlayMatch();
        }

        return this._game is Game ? this._game.GetWinnersAllRound() : new List<Team>();
    }

    public Game? GetGame()
    {
        return this._game;
    }
}