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
            IJoinable joinable = (IJoinable)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(IJoinable));

            Game game = new Game(boxGenerator, teamGenerator, joinable);

            Events.MainEvent mainEvent = new Events.MainEvent();

            mainEvent.Start(game, graphicinterface);

            graphicinterface.GameOver(game);
        }

    }
}