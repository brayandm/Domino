// Esta interfaz representa la logica de una ronda del juego
public interface IRoundGame : IBaseInterface, ISelector
{
    // Esta funcion retorna un evento de ronda del juego
    Event GetRoundGame();
}

// Esta clase representa la clasica ronda
public class ClassicRoundGame : IRoundGame
{
    public Event GetRoundGame()
    {
        return new Events.ClassicRoundGameEvent();
    }
}

// Esta clase representa la ronda donde se puede jugar mientras tengas donde jugar
public class PlayWhilePossibleRoundGame : IRoundGame
{
    public Event GetRoundGame()
    {
        return new Events.PlayWhilePossibleRoundGameEvent();
    }
}