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

    public class ClassicRoundGame : ComplexEvent, IRoundGame
    {
        public ClassicRoundGame()
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
            AddEdge(processCurrentTurn, gameOver, States.IsRoundGameOver);
            AddEdge(processCurrentTurn, reversePlayerOrder, States.IsConditionMetToReverse);
            AddEdge(processCurrentTurn, nextPlayer, States.Identity);
            AddEdge(nextPlayer, processCurrentTurn, States.Identity);

            this.Ends.Add(gameOver);
        }
    }

    public class PlayWhilePossibleRoundGame : ComplexEvent, IRoundGame
    {
        public PlayWhilePossibleRoundGame()
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
            AddEdge(processCurrentTurn, gameOver, States.IsRoundGameOver);
            AddEdge(processCurrentTurn, processCurrentTurn, States.IsNotLastPlayerPassed);
            AddEdge(processCurrentTurn, reversePlayerOrder, States.IsConditionMetToReverse);
            AddEdge(processCurrentTurn, nextPlayer, States.Identity);
            AddEdge(nextPlayer, processCurrentTurn, States.Identity);

            this.Ends.Add(gameOver);
        }
    }

    public class ClassicGame : ComplexEvent, IGame
    {
        public ClassicGame()
        {
            Event newGame = new NewGame();
            Event roundGame = (Event)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(IRoundGame));
            Event gameOver = new GameOver();

            this.Origin = newGame;

            AddEdge(newGame, roundGame, States.Identity);
            AddEdge(roundGame, gameOver, States.IsGameOver);
            AddEdge(roundGame, roundGame, States.Identity);

            this.Ends.Add(gameOver);
        }
    }

    public class MainEvent : ComplexEvent
    {
        public MainEvent()
        {
            Event game = (Event)DependencyContainerRegister.Register.Organizer.GetInstanceFromDefault(typeof(IGame));
        
            this.Origin = game;
        }
    }
}