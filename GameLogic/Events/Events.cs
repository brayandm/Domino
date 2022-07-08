class Events
{
    public class NewRoundGame : Event
    {
        public override void Action(Game game)
        {
            game.NewRoundGame();
        }
    }

    public class RoundGameOver : Event
    {
        public override void Action(Game game)
        {
            game.EndRoundGame();
        }
    }

    public class NewGame : Event
    {
        public override void Action(Game game)
        {
        }
    }

    public class GameOver : Event
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

    public class DrawTokenCurrentPlayer : Event
    {
        public override void Action(Game game)
        {
            game.DrawTokenCurrentPlayer();
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

    public class ClassicRoundGame : ComplexEvent, IRoundGame
    {
        public ClassicRoundGame()
        {
            Event newRoundGame = new NewRoundGame();
            Event distributeTokens = new DistributeTokens();
            Event processCurrentTurn = new ProcessCurrentTurn();
            Event drawTokenCurrentPlayer = new DrawTokenCurrentPlayer();
            Event nextPlayer = new NextPlayer();
            Event reversePlayerOrder = new ReversePlayerOrder();
            Event roundGameOver = new RoundGameOver();

            this.Origin = newRoundGame;

            AddEdge(newRoundGame, distributeTokens, States.Identity);
            AddEdge(distributeTokens, processCurrentTurn, States.Identity);
            AddEdge(processCurrentTurn, roundGameOver, States.IsRoundGameOver);
            AddEdge(processCurrentTurn, drawTokenCurrentPlayer, States.IsDrawable);
            AddEdge(processCurrentTurn, reversePlayerOrder, States.IsConditionMetToReverse);
            AddEdge(processCurrentTurn, nextPlayer, States.Identity);
            AddEdge(nextPlayer, processCurrentTurn, States.Identity);

            this.Ends.Add(roundGameOver);
        }
    }

    public class PlayWhilePossibleRoundGame : ComplexEvent, IRoundGame
    {
        public PlayWhilePossibleRoundGame()
        {
            Event newRoundGame = new NewRoundGame();
            Event distributeTokens = new DistributeTokens();
            Event processCurrentTurn = new ProcessCurrentTurn();
            Event drawTokenCurrentPlayer = new DrawTokenCurrentPlayer();
            Event nextPlayer = new NextPlayer();
            Event reversePlayerOrder = new ReversePlayerOrder();
            Event roundGameOver = new RoundGameOver();

            this.Origin = newRoundGame;

            AddEdge(newRoundGame, distributeTokens, States.Identity);
            AddEdge(distributeTokens, processCurrentTurn, States.Identity);
            AddEdge(processCurrentTurn, roundGameOver, States.IsRoundGameOver);
            AddEdge(processCurrentTurn, processCurrentTurn, States.IsNotLastPlayerPassed);
            AddEdge(processCurrentTurn, drawTokenCurrentPlayer, States.IsDrawable);
            AddEdge(processCurrentTurn, reversePlayerOrder, States.IsConditionMetToReverse);
            AddEdge(processCurrentTurn, nextPlayer, States.Identity);
            AddEdge(nextPlayer, processCurrentTurn, States.Identity);

            this.Ends.Add(roundGameOver);
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