interface IRoundGame : IBaseInterface, ISelector
{
    Event GetRoundGame();
}

class ClassicRoundGame
{
    public Event GetRoundGame()
    {
        return new Events.ClassicRoundGame();
    }
}

class PlayWhilePossibleRoundGame
{
    public Event GetRoundGame()
    {
        return new Events.PlayWhilePossibleRoundGame();
    }
}