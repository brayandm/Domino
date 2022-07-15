public class Program
{ 
    public static void Main()
    {
        ConsoleInterface consoleInterface = new ConsoleInterface();

        GameLogic gameLogic = new GameLogic(consoleInterface);
    }
}