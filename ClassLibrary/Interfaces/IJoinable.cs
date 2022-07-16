using System.Diagnostics;

// Esta interfaz representa el criterio de union de tokens
public interface IJoinable : IBaseInterface, ISelector
{
    // Esta funcion indica si dos tokens se pueden unir
    bool IsJoinable(Game game, ProtectedToken? protectedTokenA, ProtectedToken? protectedTokenB);
}

// Esta clase representa la clasica union por caras
public class ClassicJoinById : IJoinable
{
    public bool IsJoinable(Game game, ProtectedToken? protectedTokenA, ProtectedToken? protectedTokenB)
    {
        if(protectedTokenA is ProtectedToken && protectedTokenB is ProtectedToken)
        {
            IFace faceA = protectedTokenA.GetTokenWithoutVisibility().Faces.Item2;
            IFace faceB = protectedTokenB.GetTokenWithoutVisibility().Faces.Item1;

            return game.IsIdJoinable(faceA.Id, faceB.Id);
        }

        return true;
    }
}

// Esta clase representa la union si son tokens de equipos distintos y mismo ID
public class JoinByIdAndDifferentTeam : IJoinable
{
    public bool IsJoinable(Game game, ProtectedToken? protectedTokenA, ProtectedToken? protectedTokenB)
    {
        if(protectedTokenA is ProtectedToken && protectedTokenB is ProtectedToken)
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

                return game.IsIdJoinable(faceA.Id, faceB.Id) && teamA != teamB;
            }

            return false;
        }

        return true;
    }
}