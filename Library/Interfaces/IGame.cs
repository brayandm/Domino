interface IGame : IBaseInterface
{
    Event GetGame();
}

class ClassicGame : IGame
{
    public Event GetGame()
    {
        return new Events.ClassicGame();
    }
}