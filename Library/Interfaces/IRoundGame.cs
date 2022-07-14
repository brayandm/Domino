interface IRoundGame : IBaseInterface, ISelector
{
    Event GetRoundGame();
}

class ClassicRoundGame : IRoundGame
{
    public Event GetRoundGame()
    {
        return new Events.ClassicRoundGame();
    }
}

class PlayWhilePossibleRoundGame : IRoundGame
{
    public Event GetRoundGame()
    {
        return new Events.PlayWhilePossibleRoundGame();
    }
}