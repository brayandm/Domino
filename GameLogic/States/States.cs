class States
{
    public bool IsAllPlayersPassed(Game game)
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

    public bool Identity(Game game)
    {
        return true;
    }
}