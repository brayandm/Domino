using System.Diagnostics;

// Esta clase representa un partido de un torneo
public class Match
{
    private Game? _game;

    private List<Team> _teams = new List<Team>();

    // Esta funcion returna los equipos del partido
    public List<Team> GetTeams()
    {
        return this._teams;
    }

    // Esta funcion agrega un equipo al partido
    public void AddTeam(Team team)
    {
        this._teams.Add(team);
    }

    // Esta funcion remueve un equipo del partido
    public void RemoveTeam(Team team)
    {
        this._teams.Remove(team);
    }

    // Esta funcion agrega equipos al partido
    public void AddTeams(List<Team> teams)
    {
        foreach(Team team in teams)
        {
            this.AddTeam(team);
        }
    }

     // Esta funcion remueve equipos del partido
    public void RemoveTeams(List<Team> teams)
    {
        foreach(Team team in teams)
        {
            this.RemoveTeam(team);
        }
    }

    // Esta funcion inicia el juego
    public void PlayMatch()
    {
        Debug.Assert(this._teams.Count > 0);

        ((IGraphicInterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicInterface))).NewGame();

        this._game = new Game(this._teams);

        Events.MainEvent mainEvent = new Events.MainEvent();

        mainEvent.Start(this._game, ((IGraphicInterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicInterface))));

        ((IGraphicInterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicInterface))).GameOver(this._game);
    }

    // Esta funcion retorna los ganadores del partido
    public List<Team> GetWinners()
    {
        if(this._game is not Game)
        {
            this.PlayMatch();
        }

        return this._game is Game ? this._game.GetWinnersAllRound() : new List<Team>();
    }

    // Esta funcion retorna el juego en si
    public Game? GetGame()
    {
        return this._game;
    }
}