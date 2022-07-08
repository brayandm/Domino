class GameLogic
{
    public GameLogic()
    {
        IGraphicinterface graphicinterface = new ConsoleInterface();

        graphicinterface.Main();

        while(true)
        {
            try
            {
                graphicinterface.NewGame();

                Game game = new Game();

                Events.MainEvent mainEvent = new Events.MainEvent();

                mainEvent.Start(game, graphicinterface);

                graphicinterface.GameOver(game);
            }
            catch (TokenDealerUnavailabilityException)
            {
                Console.WriteLine("The number of tokens in the box is insufficient to start the game\n\n");
            }
        }

    }
}