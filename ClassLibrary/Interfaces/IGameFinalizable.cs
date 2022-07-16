using System.Diagnostics;

// Esta interfaz representa la regla de finalizacion del juego
public interface IGameFinalizable : IBaseInterface, ISelector
{
    // Esta funcion indica si la condicion de finalizacion se cumple
    bool IsGameFinalizable(Game game);
}

// Esta clase representa un juego de 10 rondas
public class ClassicTenRoundGameFinalizable : IGameFinalizable
{
    public bool IsGameFinalizable(Game game)
    {
        Debug.Assert(game.GetNumberOfRounds() <= 10);
        
        return game.GetNumberOfRounds() == 10;
    }
}

// Esta clase representa un juego de 3 rondas
public class ThreeRoundGameFinalizable : IGameFinalizable
{
    public bool IsGameFinalizable(Game game)
    {
        Debug.Assert(game.GetNumberOfRounds() <= 3);
        
        return game.GetNumberOfRounds() == 3;
    }
}

// Esta clase representa un juego de 1 ronda
public class OneRoundGameFinalizable : IGameFinalizable
{
    public bool IsGameFinalizable(Game game)
    {
        Debug.Assert(game.GetNumberOfRounds() <= 1);
        
        return game.GetNumberOfRounds() == 1;
    }
}

// Esta clase representa un juego con maximo puntuacion 100 puntos
public class HundredPointsGameFinalizable : IGameFinalizable
{
    public bool IsGameFinalizable(Game game)
    {
        foreach(Team team in game.GetAllTeams())
        {
            if(game.GetTeamAllRoundScore(team) >= 100)
            {
                return true;
            }
        }

        return false;
    }
}