interface IScorePlayer : IBaseInterface, ISelector
{
    int GetScore(Game game, Player player);
}

class ScorePlayerSumRule : IScorePlayer
{
    public int GetScore(Game game, Player player) 
    {
        int score = 0;

        foreach(ProtectedToken token in game.GetPlayerBoard(player).GetTokens())
        {
            score += token.GetTokenWithoutVisibility().GetTotalValue();
        }

        return score;
    }
}

class ScorePlayerMaxRule : IScorePlayer
{
    public int GetScore(Game game, Player player)
    {
        List<int> totalValues = new List<int>();

        foreach(ProtectedToken token in game.GetPlayerBoard(player).GetTokens())
        {
            totalValues.Add(token.GetTokenWithoutVisibility().GetTotalValue());
        }

        totalValues.Sort();

        return totalValues.Count != 0 ? totalValues.Last() : 0;
    }
}

class PassesScorePlayer : IScorePlayer
{
    public int GetScore(Game game, Player player) 
    {
        return game.GetPlayerTotalPassedTurns(player);
    }
}