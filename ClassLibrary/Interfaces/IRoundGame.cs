public interface IRoundGame : IBaseInterface, ISelector
{
    Event GetRoundGame();
}

public class ClassicRoundGame : IRoundGame
{
    public Event GetRoundGame()
    {
        return new Events.ClassicRoundGameEvent();
    }
}

public class PlayWhilePossibleRoundGame : IRoundGame
{
    public Event GetRoundGame()
    {
        return new Events.PlayWhilePossibleRoundGameEvent();
    }
}