// Esta clase representa la logica del juego
public class GameLogic
{
    // Constructor de la clase
    public GameLogic(IGraphicInterface graphicInterface)
    {
        DependencyContainerRegister.Getter.Initialize(typeof(IGraphicInterface), graphicInterface);

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
                ((IGraphicInterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicInterface))).SendMessage("Exception", "The number of tokens in the box is insufficient to start the game\n\n");
            }
        }
    }
}