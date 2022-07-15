public interface IRoundGame : IBaseInterface, ISelector
{
    Event GetRoundGame();
}

class ClassicRoundGame : IRoundGame
{
    public Event GetRoundGame()
    {
        return new Events.ClassicRoundGameEvent();
    }
}

class PlayWhilePossibleRoundGame : IRoundGame
{
    public Event GetRoundGame()
    {
        return new Events.PlayWhilePossibleRoundGameEvent();
    }
}