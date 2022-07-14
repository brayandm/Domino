interface IRoundFinalizationRule : IBaseInterface, ISelector
{
    bool IsRoundGameOver(Game game);
}

class ClassicFinalizationRule : IRoundFinalizationRule 
{
    public bool IsRoundGameOver(Game game) 
    {
        return game.CurrentPlayerPlayedAllTokens() || game.GetNumberOfContiguousPassedTurns() == game.GetNumberOfPlayers() || game.IsCurrentRoundEnded();
    }
}

class DoubleFinalizationRule : IRoundFinalizationRule 
{
    public bool IsRoundGameOver(Game game) 
    {
        if(game.CurrentPlayerPlayedAllTokens() || game.IsCurrentRoundEnded())
        {
            return true;
        }

        Move? move = game.GetLastMove();

        if(move != null && move.Token != null && move.Token.GetTokenWithoutVisibility().IsDouble()) 
        {
            return true;
        }

        return false;
    }
}












