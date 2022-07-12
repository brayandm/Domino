class TournamentHistory
{
    private Dictionary<Team, int> _teamMatchesWon = new Dictionary<Team, int>();
    private Dictionary<Team, int> _teamTotalScore = new Dictionary<Team, int>();

    public void TeamWinMatch(Team team)
    {
        if(!this._teamMatchesWon.ContainsKey(team))
        {
            this._teamMatchesWon.Add(team, 0);
        }

        this._teamMatchesWon[team]++;
    }

    public int GetTeamMatchesWon(Team team)
    {
        if(!this._teamMatchesWon.ContainsKey(team))
        {
            this._teamMatchesWon.Add(team, 0);
        }

        return this._teamMatchesWon[team];
    }

    public void AddTeamTotalScore(Team team, int score)
    {
        if(!this._teamTotalScore.ContainsKey(team))
        {
            this._teamTotalScore.Add(team, 0);
        }

        this._teamTotalScore[team] += score;
    }

    public int GetTeamTotalScore(Team team)
    {
        if(!this._teamTotalScore.ContainsKey(team))
        {
            this._teamTotalScore.Add(team, 0);
        }

        return this._teamTotalScore[team];
    }
}