using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

public static public class IO
{
    public static void cout<T>(T x)
    {
        Console.Write(x);
    }

    private static string get()
    {
        StringBuilder cad = new StringBuilder();
        bool flag = false;
        while(true)
        {
            int c = Console.Read();
            if(!flag)
            {
                if(c == ' ' || c == '\t' || c == '\n')continue;
                else
                {
                    flag = true;
                    cad.Append((char)c);
                }
            }
            else
            {
                if(c == ' ' || c == '\t' || c == '\n')break;
                else cad.Append((char)c);
            }
        }
        return cad.ToString();
    }

    public static void cin(ref char x)
    {
        x = (char)Console.Read();
    }

    public static void cin(ref int x)
    {
        x = int.Parse(get());
    }

    public static void cin(ref long x)
    {
        x = long.Parse(get());
    }

    public static void cin(ref float x)
    {
        x = float.Parse(get());
    }

    public static void cin(ref double x)
    {
        x = double.Parse(get());
    }

    public static void cin(ref string x)
    {
        x = get();
    }

    public static void db<T>(List<T> data)
    {
        int cont = 0;
        Console.Write("[");
        foreach(T x in data)
        {
            if(cont > 0)Console.Write(", ");
            Console.Write(x);
            cont++;
        }
        Console.Write("]");
        Console.Write("\n");
    }

    public static void db<T,K>(List<Tuple<T,K>> data)
    {
        int cont = 0;
        Console.Write("[");
        foreach(Tuple<T,K> x in data)
        {
            if(cont > 0)Console.Write(", ");
            Console.Write("(" + x.Item1 + ", " + x.Item2 + ")");
            cont++;
        }
        Console.Write("]");
        Console.Write("\n");
    }

    public static void db<T,K,Q>(List<Tuple<T,K,Q>> data)
    {
        int cont = 0;
        Console.Write("[");
        foreach(Tuple<T,K,Q> x in data)
        {
            if(cont > 0)Console.Write(", ");
            Console.Write("(" + x.Item1 + ", " + x.Item2 + ", " + x.Item3 + ")");
            cont++;
        }
        Console.Write("]");
        Console.Write("\n");
    }
}