using System.Diagnostics;

interface IGameFinalizable : IBaseInterface, ISelector
{
    bool IsGameFinalizable(Game game);
}

class ClassicTenRoundGameFinalizable : IGameFinalizable
{
    public bool IsGameFinalizable(Game game)
    {
        Debug.Assert(game.GetNumberOfRounds() <= 10);
        
        return game.GetNumberOfRounds() == 10;
    }
}

class ThreeRoundGameFinalizable : IGameFinalizable
{
    public bool IsGameFinalizable(Game game)
    {
        Debug.Assert(game.GetNumberOfRounds() <= 3);
        
        return game.GetNumberOfRounds() == 3;
    }
}

class OneRoundGameFinalizable : IGameFinalizable
{
    public bool IsGameFinalizable(Game game)
    {
        Debug.Assert(game.GetNumberOfRounds() <= 1);
        
        return game.GetNumberOfRounds() == 1;
    }
}

class HundredPointsGameFinalizable : IGameFinalizable
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