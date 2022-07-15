public interface IRoundScorePlayer : IBaseInterface, ISelector
{
    int GetScore(Game game, Player player);
}

class RoundScorePlayerSumRule : IRoundScorePlayer
{
    public int GetScore(Game game, Player player) 
    {
        int score = 0;

        foreach(ProtectedToken token in game.GetPlayerBoard(player).GetTokens())
        {
            score += token.GetTokenWithoutVisibility().GetValue();
        }

        return score;
    }
}

class RoundScorePlayerMaxRule : IRoundScorePlayer
{
    public int GetScore(Game game, Player player)
    {
        List<int> totalValues = new List<int>();

        foreach(ProtectedToken token in game.GetPlayerBoard(player).GetTokens())
        {
            totalValues.Add(token.GetTokenWithoutVisibility().GetValue());
        }

        totalValues.Sort();

        return totalValues.Count != 0 ? totalValues.Last() : 0;
    }
}

class RoundPassesScorePlayer : IRoundScorePlayer
{
    public int GetScore(Game game, Player player) 
    {
        return game.GetPlayerTotalPassedTurns(player);
    }
}