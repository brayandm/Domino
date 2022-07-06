using System.Diagnostics;

interface IIdJoinable : IBaseInterface, ISelector
{
    bool IsIdJoinable(string IdA, string idB);
}

class ClassicIdJoinable : IIdJoinable
{
    public bool IsIdJoinable(string IdA, string idB)
    {
        return true;
    }
}

class DivisibilityIdJoinable : IIdJoinable
{
    public bool IsIdJoinable(string IdA, string IdB)
    {
        try
        {
            int intA = 0;
            int intB = 0;

            foreach(char c in IdA)
            {
                if(!char.IsDigit(c))
                {
                    Debug.Assert(false);
                }
                intA = intA * 10 + (int)char.GetNumericValue(c);
            }

            foreach(char c in IdB)
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