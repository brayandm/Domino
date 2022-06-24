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
}