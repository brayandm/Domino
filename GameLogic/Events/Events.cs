class Events
{
    public class NewGame : Event
    {
        public override void Action(Game game)
        {
        }
    }

    public class DistributeTokens : Event
    {
        public override void Action(Game game)
        {
            ITokenDealer tokenDealer = (ITokenDealer)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(ITokenDealer));

            game.DistributeTokens(tokenDealer);
        }
    }

    public class ProcessCurrentTurn : Event
    {
        public override void Action(Game game)
        {
            game.ProcessCurrentTurn();
        }
    }

    public class NextPlayer : Event
    {
        public override void Action(Game game)
        {
            game.NextPlayer();
        }
    }

    public class ReversePlayerOrder : Event
    {
        public override void Action(Game game)
        {
            game.ReversePlayerOrder();
        }
    }

    public class GameOver : Event
    {
        public override void Action(Game game)
        {
        }
    }

    public class ClassicGame : ComplexEvent, IRoundGame
    {
        public ClassicGame()
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

    public class PlayWhilePossibleGame : ComplexEvent, IRoundGame
    {
        public PlayWhilePossibleGame()
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
            AddEdge(processCurrentTurn, processCurrentTurn, States.IsNotLastPlayerPassed);
            AddEdge(processCurrentTurn, reversePlayerOrder, States.IsConditionMetToReverse);
            AddEdge(processCurrentTurn, nextPlayer, States.Identity);
            AddEdge(nextPlayer, processCurrentTurn, States.Identity);

            this.Ends.Add(gameOver);
        }
    }

    public class MainEvent : ComplexEvent
    {
        public MainEvent()
        {
            Event game = (Event)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(IRoundGame));
        
            this.Origin = game;
        }
    }
}