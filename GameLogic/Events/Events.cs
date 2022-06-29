class Events
{
    private class NewGame : Event
    {
        public override void Action(Game game)
        {
        }
    }

    private class DistributeTokens : Event
    {
        public override void Action(Game game)
        {
            ITokenDealer tokenDealer = (ITokenDealer)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(ITokenDealer));

            game.DistributeTokens(tokenDealer);
        }
    }

    private class ProcessCurrentTurn : Event
    {
        public override void Action(Game game)
        {
            game.ProcessCurrentTurn();
        }
    }

    private class NextPlayer : Event
    {
        public override void Action(Game game)
        {
            game.NextPlayer();
        }
    }

    private class ReversePlayerOrder : Event
    {
        public override void Action(Game game)
        {
            game.ReversePlayerOrder();
        }
    }

    private class GameOver : Event
    {
        public override void Action(Game game)
        {
        }
    }

    public class MainEvent : ComplexEvent
    {
        public MainEvent()
        {
            Event newGame = new NewGame();
            Event distributeTokens = new DistributeTokens();
            Event processCurrentTurn = new ProcessCurrentTurn();
            Event nextPlayer = new NextPlayer();
            Event reversePlayerOrder = new ReversePlayerOrder();
            Event gameOver = new GameOver();

            this.Origin = newGame;

            AddEdge(newGame, distributeTokens, States.Identity);
            AddEdge(distributeTokens, processCurrentTurn, States.Identity);
            AddEdge(processCurrentTurn, gameOver, States.IsGameOver);
            AddEdge(processCurrentTurn, reversePlayerOrder, States.IsConditionMetToReverse);
            AddEdge(processCurrentTurn, nextPlayer, States.Identity);
            AddEdge(nextPlayer, processCurrentTurn, States.Identity);

            this.Ends.Add(gameOver);
        }
    }
}