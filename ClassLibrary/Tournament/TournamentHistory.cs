// Esta clase reptresenta la historia de un juego
public class TournamentHistory
{
    private Dictionary<Team, int> _teamMatchesWon = new Dictionary<Team, int>();
    private Dictionary<Team, int> _teamTotalScore = new Dictionary<Team, int>();
    private int _numberOfMatches = 0;

    // Esta funcion agrega un nuevo partido al historial
    public void AddMatch()
    {
        this._numberOfMatches++;
    }

    // Esta funcion retorna el numero de partidos pasta ahora creo

    public int GetNumberOfMatches()
    {
        return this._numberOfMatches;
    }

    // Esta funcion agrega un win match a un score nuestro
    public void TeamWinMatch(Team team)
    {
        if(!this._teamMatchesWon.ContainsKey(team))
        {
            this._teamMatchesWon.Add(team, 0);
        }

        this._teamMatchesWon[team]++;
    }

    // Esta funcion returna los juegos ganados por el equipo
    public int GetTeamMatchesWon(Team team)
    {
        if(!this._teamMatchesWon.ContainsKey(team))
        {
            this._teamMatchesWon.Add(team, 0);
        }

        return this._teamMatchesWon[team];
    }

    // Esta funcion agrega el total score a un equipo
    public void AddTeamTotalScore(Team team, int score)
    {
        if(!this._teamTotalScore.ContainsKey(team))
        {
            this._teamTotalScore.Add(team, 0);
        }

        this._teamTotalScore[team] += score;
    }

    // Esta funcion retorna el total de puntaje de un equipo
    public int GetTeamTotalScore(Team team)
    {
        if(!this._teamTotalScore.ContainsKey(team))
        {
            this._teamTotalScore.Add(team, 0);
        }

        return this._teamTotalScore[team];
    }
}