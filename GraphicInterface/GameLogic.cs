public class GameLogic
{
    public GameLogic()
    {
        DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IGraphicInterface), typeof(ConsoleInterface));
        DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IObjectsGraphic), typeof(ObjectsGraphic));

        DependencyContainerRegister.Getter.Initialize(typeof(IGraphicInterface));
        DependencyContainerRegister.Getter.Initialize(typeof(IObjectsGraphic));

        ((IGraphicInterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicInterface))).Main();

        while(true)
        {
            try
            {
                ((IGraphicInterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicInterface))).NewTournament();

                ITournamentGenerator tournamentGenerator = (ITournamentGenerator)DependencyContainerRegister.Getter.GetInstance(typeof(ITournamentGenerator));

                Tournament tournament = tournamentGenerator.GetTournament();

                tournament.StartTournament();

                ((IGraphicInterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicInterface))).TournamentOver(tournament);
            }
            catch (TokenDealerUnavailabilityException)
            {
                Console.WriteLine("The number of tokens in the box is insufficient to start the game\n\n");
            }
        }
    }
}