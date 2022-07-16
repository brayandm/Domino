using System.Diagnostics;

// Esta interfaz representa el criterio de union de caras
public interface IIdJoinable : IBaseInterface, ISelector
{
    // Esta funcion indica si dos caras son compatibles
    bool IsIdJoinable(string idA, string idB);
}

// Esta clase representa el clasico comparador de ID
public class ClassicIdJoinable : IIdJoinable
{
    public bool IsIdJoinable(string idA, string idB)
    {
        return idA == idB;
    }
}

// Esta clase representa comparar ID por divisibilidad
public class DivisibilityIdJoinable : IIdJoinable
{
    public bool IsIdJoinable(string idA, string idB)
    {
        try
        {
            int intA = 0;
            int intB = 0;

            foreach(char c in idA)
            {
                if(!char.IsDigit(c))
                {
                    Debug.Assert(false);
                }
                intA = intA * 10 + (int)char.GetNumericValue(c);
            }

            foreach(char c in idB)
            {
                if(!char.IsDigit(c))
                {
                    Debug.Assert(false);
                }
                intB = intB * 10 + (int)char.GetNumericValue(c);
            }

            if(intB != 0 && intA % intB == 0)
            {
                return true;
            }

            if(intA != 0 && intB % intA == 0)
            {
                return true;
            }
        
            return false;
        }
        catch
        {
            return false;
        }
    }
}