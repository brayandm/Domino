public class Program
{ 
    // Clase donde se empieza a ejecutar todo
    public static void Main()
    {
        ConsoleInterface consoleInterface = new ConsoleInterface();

        GameLogic gameLogic = new GameLogic(consoleInterface);
    }
}