public interface IRoundScoreTeam : IBaseInterface, ISelector
{
    int GetScore(Game game, Team team);
}

class RoundScoreTeamSumRule : IRoundScoreTeam
{
    public int GetScore(Game game, Team team)
    {
        int total = 0;
        
        foreach(Player player in team)
        {
            total += game.GetRoundPlayerScore(player);
        }

        return total;
    }
}

class RoundScoreTeamMaxRule : IRoundScoreTeam
{
    public int GetScore(Game game, Team team)
    {
        List<int> scores = new List<int>();

        foreach(Player player in team)
        {
            scores.Add(game.GetRoundPlayerScore(player));
        }

        scores.Sort();

        return scores.Count != 0 ? scores.Last() : 0;
    }
}