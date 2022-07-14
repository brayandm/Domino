class Events
{
    public class DoNothing : Event
    {
        public override void Action(Game game)
        {
            
        }
    }

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
            game.SetGameToEnded();
        }
    }

    public class DistributeTokens : Event
    {
        public override void Action(Game game)
        {
            ITokenDealer tokenDealer = (ITokenDealer)DependencyContainerRegister.Getter.GetInstance(typeof(ITokenDealer));

            game.DistributeTokens(tokenDealer);
        }
    }

    public class ProcessCurrentTurn : Event
    {
        public override void Action(Game game)
        {
            if(game.PowerHandler.GetActivity("PassNextPlayerTurnPower"))
            {
                return;
            }

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

    public class ClassicRoundGameEvent : ComplexEvent
    {
        public ClassicRoundGameEvent()
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
            AddEdge(processCurrentTurn, drawTokenCurrentPlayer, States.IsDrawable);
            AddEdge(processCurrentTurn, roundGameOver, States.IsRoundGameOver);
            AddEdge(processCurrentTurn, reversePlayerOrder, States.IsConditionMetToReverse);
            AddEdge(processCurrentTurn, nextPlayer, States.Identity);
            AddEdge(nextPlayer, processCurrentTurn, States.Identity);

            this.Ends.Add(roundGameOver);
        }
    }

    public class PlayWhilePossibleRoundGameEvent : ComplexEvent
    {
        public PlayWhilePossibleRoundGameEvent()
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
            AddEdge(processCurrentTurn, drawTokenCurrentPlayer, States.IsDrawable);
            AddEdge(processCurrentTurn, roundGameOver, States.IsRoundGameOver);
            AddEdge(processCurrentTurn, processCurrentTurn, States.IsNotLastPlayerPassed);
            AddEdge(processCurrentTurn, reversePlayerOrder, States.IsConditionMetToReverse);
            AddEdge(processCurrentTurn, nextPlayer, States.Identity);
            AddEdge(nextPlayer, processCurrentTurn, States.Identity);

            this.Ends.Add(roundGameOver);
        }
    }

    public class ClassicGameEvent : ComplexEvent
    {
        public ClassicGameEvent()
        {
            Event newGame = new NewGame();
            Event roundGame = (DependencyContainerRegister.Getter.GetInstance(typeof(IRoundGame))).GetRoundGame();
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
            Event game = ((IGame)DependencyContainerRegister.Getter.GetInstance(typeof(IGame))).GetGame();

            this.Origin = game;
        }
    }
}