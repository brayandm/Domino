using System.Threading;

interface IGraphicinterface
{
    void Main();
    void NewGame();
    void GameOver(Game game);
    void Update(Game game);
}

class ConsoleInterface : IGraphicinterface
{
    public void Main()
    {
        Console.WriteLine("Welcome to Domino game");
        Thread.Sleep(5000);
    }

    public void NewGame()
    {
        Console.WriteLine("The Domino game will be set with default configuration");
    }

    public void GameOver(Game game)
    {

    }

    public void Update(Game game)
    {

    }
}