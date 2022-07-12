class TournamentHistory
{
    private Dictionary<Team, int> _teamMatchsWon = new Dictionary<Team, int>();
    private Dictionary<Team, int> _teamTotalScore = new Dictionary<Team, int>();

    public void TeamWinMatch(Team team)
    {
        if(!this._teamMatchsWon.ContainsKey(team))
        {
            this._teamMatchsWon.Add(team, 0);
        }

        this._teamMatchsWon[team]++;
    }

    public int GetTeamMatchsWon(Team team)
    {
        if(!this._teamMatchsWon.ContainsKey(team))
        {
            this._teamMatchsWon.Add(team, 0);
        }

        return this._teamMatchsWon[team];
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