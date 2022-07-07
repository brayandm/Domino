interface IScoreTeam : IBaseInterface, ISelector
{
    int GetScore(Game game, Team team);
}

class ScoreTeamSumRule : IScoreTeam
{
    public int GetScore(Game game, Team team)
    {
        List<int> scores = game.GetTeamAllRoundScores(team);

        int total = 0;
        
        foreach(int score in scores)
        {
            total += score;
        }

        return total;
    }
}

class ScoreTeamMaxRule : IScoreTeam
{
    public int GetScore(Game game, Team team)
    {
        List<int> scores = game.GetTeamAllRoundScores(team);

        scores.Sort();

        return scores.Count != 0 ? scores.Last() : 0;
    }
}