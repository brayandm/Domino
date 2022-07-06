using System.Diagnostics;

interface IJoinable : IBaseInterface, ISelector
{
    bool IsJoinable(Game game, ProtectedToken protectedTokenA, ProtectedToken protectedTokenB);
}

class ClassicJoinById : IJoinable
{
    public bool IsJoinable(Game game, ProtectedToken protectedTokenA, ProtectedToken protectedTokenB)
    {
        IFace faceA = protectedTokenA.GetTokenWithoutVisibility().Faces.Item2;
        IFace faceB = protectedTokenB.GetTokenWithoutVisibility().Faces.Item1;

        return faceA.Id == faceB.Id;
    }
}

class JoinByIdAndDifferentTeam : IJoinable
{
    public bool IsJoinable(Game game, ProtectedToken protectedTokenA, ProtectedToken protectedTokenB)
    {
        IFace faceA = protectedTokenA.GetTokenWithoutVisibility().Faces.Item2;
        IFace faceB = protectedTokenB.GetTokenWithoutVisibility().Faces.Item1;

        Player? playerA = protectedTokenA.GetCurrentOwner();
        Player? playerB = protectedTokenB.GetCurrentOwner();

        Debug.Assert(playerA is Player);
        Debug.Assert(playerB is Player);

        if(playerA is Player && playerB is Player)
        {
            Team teamA = game.GetPlayerTeam(playerA);
            Team teamB = game.GetPlayerTeam(playerB);

            return faceA.Id == faceB.Id && teamA != teamB;
        }

        return false;
    }
}