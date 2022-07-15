public interface IGame : IBaseInterface
{
    Event GetGame();
}

public class ClassicGame : IGame
{
    public Event GetGame()
    {
        return new Events.ClassicGameEvent();
    }
}