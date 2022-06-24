class GameLogic
{
    public GameLogic(Game game)
    {
        Events.MainEvent mainEvent = new Events.MainEvent();

        mainEvent.Start(game);
    }
}