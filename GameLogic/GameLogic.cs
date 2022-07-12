class GameLogic
{
    public GameLogic()
    {
        Graphics.graphicinterface.Main();

        while(true)
        {
            try
            {
                Graphics.graphicinterface.NewGame();

                Game game = new Game();

                Events.MainEvent mainEvent = new Events.MainEvent();

                mainEvent.Start(game, Graphics.graphicinterface);

                Graphics.graphicinterface.GameOver(game);
            }
            catch (TokenDealerUnavailabilityException)
            {
                Console.WriteLine("The number of tokens in the box is insufficient to start the game\n\n");
            }
        }
    }
}