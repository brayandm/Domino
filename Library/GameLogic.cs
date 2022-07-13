class GameLogic
{
    public GameLogic()
    {
        Graphics.graphicinterface.Main();

        while(true)
        {
            try
            {
                Graphics.graphicinterface.NewTournament();

                ITournamentGenerator tournamentGenerator = (ITournamentGenerator)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(ITournamentGenerator));

                Tournament tournament = tournamentGenerator.GetTournament();

                tournament.StartTournament();

                Graphics.graphicinterface.TournamentOver(tournament);
            }
            catch (TokenDealerUnavailabilityException)
            {
                Console.WriteLine("The number of tokens in the box is insufficient to start the game\n\n");
            }
        }
    }
}