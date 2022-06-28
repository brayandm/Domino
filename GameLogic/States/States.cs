class States
{
    public static bool IsAllPlayersPassed(Game game)
    {
        if(game.GetNumberOfContiguousPassedTurns() >= game.GetNumberOfPlayers())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool CurrentPlayerPlayedAllTokens(Game game)
    {
        if(game.CurrentPlayerPlayedAllTokens() == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool Identity(Game game)
    {
        return true;
    }
}