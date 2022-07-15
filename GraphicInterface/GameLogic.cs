class GameLogic
{
    public GameLogic()
    {
        ((IGraphicinterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicinterface))).Main();

        while(true)
        {
            try
            {
                ((IGraphicinterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicinterface))).NewTournament();

                ITournamentGenerator tournamentGenerator = (ITournamentGenerator)DependencyContainerRegister.Getter.GetInstance(typeof(ITournamentGenerator));

                Tournament tournament = tournamentGenerator.GetTournament();

                tournament.StartTournament();

                ((IGraphicinterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicinterface))).TournamentOver(tournament);
            }
            catch (TokenDealerUnavailabilityException)
            {
                Console.WriteLine("The number of tokens in the box is insufficient to start the game\n\n");
            }
        }
    }
}