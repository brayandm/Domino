public static class Utils
{
    public static void Swap<T>(ref T a, ref T b)
    {
        T c = a;
        a = b;
        b = c;
    }

    public static void Swap<T>(T a, T b)
    {
        T c = a;
        a = b;
        b = c;
    }

    public static bool IsSort<T>(params T[] list) where T : IComparable<T>
    {
        for(int i = 1 ; i < list.Length ; i++)
        {
            if(list[i-1].CompareTo(list[i]) > 0)
            {
                return false;
            }
        }
        return true;
    }

    public static int GetIntFromConsoleKeyInfo(ConsoleKeyInfo keyInfo)
    {
        return (int)keyInfo.Key - 48;
    }

    public static bool IsEvenNumber(string cad)
    {
        try
        {
            int value = int.Parse(cad);

            return value % 2 == 0;
        }
        catch
        {
            return false;
        }
    }
}