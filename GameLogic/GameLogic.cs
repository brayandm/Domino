class GameLogic
{
    public GameLogic(Game game, IGraphicinterface graphicinterface)
    {
        Events.MainEvent mainEvent = new Events.MainEvent();

        mainEvent.Start(game, graphicinterface);
    }
}