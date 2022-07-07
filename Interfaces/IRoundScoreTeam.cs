interface IRoundScoreTeam : IBaseInterface, ISelector
{
    int GetScore(Game game, Team team, IRoundScorePlayer scorePlayer);
}

class RoundScoreTeamSumRule : IRoundScoreTeam
{
    public int GetScore(Game game, Team team, IRoundScorePlayer scorePlayer)
    {
        int total = 0;
        
        foreach(Player player in team)
        {
            total += scorePlayer.GetScore(game, player);
        }

        return total;
    }
}

class RoundScoreTeamMaxRule : IRoundScoreTeam
{
    public int GetScore(Game game, Team team, IRoundScorePlayer scorePlayer)
    {
        List<int> scores = new List<int>();

        foreach(Player player in team)
        {
            scores.Add(scorePlayer.GetScore(game, player));
        }

        scores.Sort();

        return scores.Count != 0 ? scores.Last() : 0;
    }
}