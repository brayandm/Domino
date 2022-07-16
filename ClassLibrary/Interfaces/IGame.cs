// Esta interfaz representa un juego
public interface IGame : IBaseInterface
{
    Event GetGame();
}

// Esta clase representa un juego clasico
public class ClassicGame : IGame
{
    public Event GetGame()
    {
        return new Events.ClassicGameEvent();
    }
}