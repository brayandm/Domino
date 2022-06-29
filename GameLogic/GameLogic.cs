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
            IPlayerGenerator playerGenerator = (IPlayerGenerator)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(IPlayerGenerator));

            Game game = new Game(boxGenerator, playerGenerator);

            Events.MainEvent mainEvent = new Events.MainEvent();

            mainEvent.Start(game, graphicinterface);

            graphicinterface.GameOver(game);
        }

    }
}