using System.Diagnostics;

public class Tournament
{
    private List<Match> _matches;

    private List<Team> _teams = new List<Team>();

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

    public TournamentHistory GetTournamentHistory()
    {
        return this._tournamentHistory;
    }

    private bool DFS(Match match, HashSet<Match> mark, HashSet<Match> markIn)
    {
        mark.Add(match);
        markIn.Add(match);

        foreach(Match nextMatch in _nextMatches[match])
        {
            if(!mark.Contains(nextMatch))
            {
                if(!DFS(nextMatch, mark, markIn))
                {
                    return false;
                }
            }
            else if(markIn.Contains(nextMatch))
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

    private void StoreTeamsFromMatches(List<Match> matches)
    {
        foreach(Match match in matches)
        {
            foreach(Team team in match.GetTeams())
            {
                if(!this._teams.Contains(team))
                {
                    this._teams.Add(team);
                }
            }
        }
    }  

    public List<Team> GetTeams()
    {
        return this._teams;
    }

    public Tournament(List<Match> matches, Dictionary<Match, List<Match>> nextMatches)
    {
        this._matches = matches;
        this._nextMatches = nextMatches;

        this.StoreTeamsFromMatches(this._matches);

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
    }

    public int GetNumberOfMatches()
    {
        return this._tournamentHistory.GetNumberOfMatches();
    }

    public void StartTournament()
    {
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
            foreach(Match nextMatch in this._nextMatches[match])
            {
                inDegree[nextMatch]++;
            }
        }

        Queue<Match> queue = new Queue<Match>();

        foreach(Match match in this._matches)
        {
            if(inDegree[match] == 0)
            {
                queue.Enqueue(match);
            }
        }

        while(queue.Count > 0)
        {
            Match match = queue.Dequeue();

            match.PlayMatch();

            this._tournamentHistory.AddMatch();

            ((IGraphicInterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicInterface))).UpdateTournament(this);

            Game? currentGame = match.GetGame();

            if(currentGame is Game)
            {
                this.UpdateTournamentHistory(currentGame);
            } 

            foreach(Match nextMatch in this._nextMatches[match])
            {
                inDegree[nextMatch]--;

                nextMatch.AddTeams(match.GetWinners());
                
                if(inDegree[nextMatch] == 0)
                {
                    queue.Enqueue(nextMatch);
                }
            }
        }
    }
}