class GameLogic
{
    public GameLogic()
    {
        IGraphicinterface graphicinterface = new ConsoleInterface();

        graphicinterface.Main();

        while(true)
        {
            graphicinterface.NewGame();
            
            ITokenGenerator tokenGenerator = (ITokenGenerator)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(ITokenGenerator));
            IFaceGenerator faceGenerator = (IFaceGenerator)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(IFaceGenerator));
            IFilterTokenRule filterTokenRule = (IFilterTokenRule)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(IFilterTokenRule));
            IPlayerGenerator playerGenerator = (IPlayerGenerator)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(IPlayerGenerator));
            IOrderPlayer orderPlayer = (IOrderPlayer)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(IOrderPlayer));

            Game game = new Game(tokenGenerator, faceGenerator, filterTokenRule, playerGenerator, orderPlayer);

            Events.MainEvent mainEvent = new Events.MainEvent();

            mainEvent.Start(game, graphicinterface);

            graphicinterface.GameOver(game);
        }

    }
}