interface IRoundFinalizationRule : IBaseInterface, ISelector
{
    bool IsGameOver(Game game);
}

class ClassicFinalizationRule : IRoundFinalizationRule 
{
    public bool IsGameOver(Game game) 
    {
        return game.CurrentPlayerPlayedAllTokens() || game.GetNumberOfContiguousPassedTurns() == game.GetNumberOfPlayers();
    }
}

class DoubleFinalizationRule : IRoundFinalizationRule 
{
    public bool IsGameOver(Game game) 
    {
        if(game.CurrentPlayerPlayedAllTokens())
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












