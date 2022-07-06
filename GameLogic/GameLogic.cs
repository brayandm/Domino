class GameLogic
{
    public GameLogic()
    {
        IGraphicinterface graphicinterface = new ConsoleInterface();

        graphicinterface.Main();

        while(true)
        {
            graphicinterface.NewGame();
            
            IBoxGenerator boxGenerator = (IBoxGenerator)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(IBoxGenerator));
            ITeamGenerator teamGenerator = (ITeamGenerator)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(ITeamGenerator));

            Game game = new Game(boxGenerator, teamGenerator);

            Events.MainEvent mainEvent = new Events.MainEvent();

            mainEvent.Start(game, graphicinterface);

            graphicinterface.GameOver(game);
        }

    }
}