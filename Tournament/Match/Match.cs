class Match
{
    Game game;

    public Match(List<Team> teams)
    {
        this.game = new Game(teams);

        Events.MainEvent mainEvent = new Events.MainEvent();

        mainEvent.Start(this.game, graphicinterface);
    }
}