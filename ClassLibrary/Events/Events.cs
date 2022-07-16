// Esta clase contiene algunos eventos
public class Events
{
    // Este evento no hace nada
    public class DoNothing : Event
    {
        public override void Action(Game game)
        {
            
        }
    }

    // Este evento crea una nueva ronda
    public class NewRoundGame : Event
    {
        public override void Action(Game game)
        {
            game.NewRoundGame();
        }
    }

    // Este evento finaliza una ronda
    public class RoundGameOver : Event
    {
        public override void Action(Game game)
        {
            game.EndRoundGame();
        }
    }

    // Este evento crea un nuevo juego
    public class NewGame : Event
    {
        public override void Action(Game game)
        {
        }
    }

    // Este evento finaliza un juego
    public class GameOver : Event
    {
        public override void Action(Game game)
        {
            game.SetGameToEnded();
        }
    }

    // Este evento reparte los tokens al inicio
    public class DistributeTokens : Event
    {
        public override void Action(Game game)
        {
            ITokenDealer tokenDealer = (ITokenDealer)DependencyContainerRegister.Getter.GetInstance(typeof(ITokenDealer));

            game.DistributeTokens(tokenDealer);
        }
    }

    // Este evento procesa el turno actual
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

    // Este evento hace que un jugador robe un token de la caja
    public class DrawTokenCurrentPlayer : Event
    {
        public override void Action(Game game)
        {
            game.DrawTokenCurrentPlayer();
        }
    }

    // Este evento hace que se pase al proximo jugador
    public class NextPlayer : Event
    {
        public override void Action(Game game)
        {
            game.NextPlayer();
        }
    }

    // Este evento invierte el orden de los turnos
    public class ReversePlayerOrder : Event
    {
        public override void Action(Game game)
        {
            game.ReversePlayerOrder();
        }
    }

    // Este evento representa una ronda clasica
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

    // Este evento representa una variante de una ronda en donde
    //se puede jugar siempre que se pueda
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

    // Este evento presenta un juego normal por rondas
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

    // Este evento es el evento principal
    public class MainEvent : ComplexEvent
    {
        public MainEvent()
        {
            Event game = ((IGame)DependencyContainerRegister.Getter.GetInstance(typeof(IGame))).GetGame();

            this.Origin = game;
        }
    }
}