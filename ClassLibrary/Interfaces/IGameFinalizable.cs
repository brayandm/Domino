using System.Diagnostics;

public interface IGameFinalizable : IBaseInterface, ISelector
{
    bool IsGameFinalizable(Game game);
}

public class ClassicTenRoundGameFinalizable : IGameFinalizable
{
    public bool IsGameFinalizable(Game game)
    {
        Debug.Assert(game.GetNumberOfRounds() <= 10);
        
        return game.GetNumberOfRounds() == 10;
    }
}

public class ThreeRoundGameFinalizable : IGameFinalizable
{
    public bool IsGameFinalizable(Game game)
    {
        Debug.Assert(game.GetNumberOfRounds() <= 3);
        
        return game.GetNumberOfRounds() == 3;
    }
}

public class OneRoundGameFinalizable : IGameFinalizable
{
    public bool IsGameFinalizable(Game game)
    {
        Debug.Assert(game.GetNumberOfRounds() <= 1);
        
        return game.GetNumberOfRounds() == 1;
    }
}

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