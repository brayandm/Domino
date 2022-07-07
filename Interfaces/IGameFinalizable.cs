using System.Diagnostics;

interface IGameFinalizable : IBaseInterface, ISelector
{
    bool IsGameFinalizable(Game game);
}

class ClassicTenRoundGameFinalizable
{
    bool IsGameFinalizable(Game game)
    {
        Debug.Assert(game.GetNumberOfRounds() <= 10);
        
        return game.GetNumberOfRounds() == 10;
    }
}

class HundredPointsGameFinalizable
{
    bool IsGameFinalizable(Game game)
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