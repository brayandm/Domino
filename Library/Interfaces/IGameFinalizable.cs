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
        
        return game.GetNumberOfRounds() == 10 || game.IsGameEnded();
    }
}

class ThreeRoundGameFinalizable : IGameFinalizable
{
    public bool IsGameFinalizable(Game game)
    {
        Debug.Assert(game.GetNumberOfRounds() <= 3);
        
        return game.GetNumberOfRounds() == 3 || game.IsGameEnded();
    }
}

class OneRoundGameFinalizable : IGameFinalizable
{
    public bool IsGameFinalizable(Game game)
    {
        Debug.Assert(game.GetNumberOfRounds() <= 1);
        
        return game.GetNumberOfRounds() == 1 || game.IsGameEnded();
    }
}

class HundredPointsGameFinalizable : IGameFinalizable
{
    public bool IsGameFinalizable(Game game)
    {
        if(game.IsGameEnded())
        {
            return true;
        }
        
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