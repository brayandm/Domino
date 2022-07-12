using System.Diagnostics;

class Tournament
{
    private List<Match> _matches;

    private Dictionary<Match, List<Match>> _nextMatches;

    private TournamentHistory _tournamentHistory = new TournamentHistory();

    private void UpdateTournamentHistory(Game game)
    {
        foreach(Team team in game.GetWinnersAllRound())
        {
            this._tournamentHistory.TeamWinMatch(team);
        }

        foreach(Team team in game.GetAllTeams())
        {
            this._tournamentHistory.AddTeamTotalScore(team, game.GetTeamAllRoundScore(team));
        }
    }

    private bool DFS(Match match, HashSet<Match> mark, HashSet<Match> markIn)
    {
        mark.Add(match);
        markIn.Add(match);

        foreach(Match nextMatch in _nextMatches[match])
        {
            if(!mark.Contains(match))
            {
                if(!DFS(nextMatch, mark, markIn))
                {
                    return false;
                }
            }
            else if(markIn.Contains(match))
            {
                return false;
            }
        }

        markIn.Remove(match);

        return true;
    }

    private bool IsDirectedAcyclicGraph()
    {
        HashSet<Match> mark = new HashSet<Match>();
        HashSet<Match> markIn = new HashSet<Match>();

        foreach(Match match in this._matches)
        {
            if(!mark.Contains(match))
            {
                if(!DFS(match, mark, markIn))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private bool CheckMatchesAndNextMatches()
    {
        List<Match> nextMatchesList = this._nextMatches.Keys.ToList();

        foreach(Match match in this._matches)
        {
            if(!nextMatchesList.Contains(match))
            {
                return false;
            }
        }

        foreach(Match match in nextMatchesList)
        {
            if(!this._matches.Contains(match))
            {
                return false;
            }
        }

        return true;
    }

    public Tournament(List<Match> matches, Dictionary<Match, List<Match>> nextMatches)
    {
        this._matches = matches;
        this._nextMatches = nextMatches;

        foreach(Match match in this._matches)
        {
            if(!this._nextMatches.ContainsKey(match))
            {
                this._nextMatches.Add(match, new List<Match>());
            }
        }

        Debug.Assert(this.CheckMatchesAndNextMatches());

        if(!this.IsDirectedAcyclicGraph())
        {
            throw new TournamentIsNotDirectedAcyclicGraph();
        }

        Dictionary<Match, int> inDegree = new Dictionary<Match, int>();

        foreach(Match match in this._matches)
        {
            if(!inDegree.ContainsKey(match))
            {
                inDegree.Add(match, 0);
            }
        }

        foreach(Match match in this._matches)
        {
            foreach(Match nextMatch in this._nextMatches.Keys)
            {
                inDegree[nextMatch]++;
            }
        }

        Queue<Match> queue = new Queue<Match>();

        foreach(Match match in this._matches)
        {
            if(inDegree[match] == 0)
            {
                queue.Append(match);
            }
        }

        while(queue.Count > 0)
        {
            Match match = queue.Dequeue();

            match.PlayMatch();

            Game? currentGame = match.GetGame();

            if(currentGame is Game)
            {
                this.UpdateTournamentHistory(currentGame);
            } 

            foreach(Match nextMatch in this._nextMatches[match])
            {
                inDegree[nextMatch]--;

                if(inDegree[nextMatch] == 0)
                {
                    nextMatch.AddTeams(match.GetWinners());
                    queue.Append(nextMatch);
                }
            }
        }
    }
}