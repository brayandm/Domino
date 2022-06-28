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
    private int _numberOfMoves = 0;

    public void Main()
    {
        Console.WriteLine("Welcome to Domino game\n\n");

        Thread.Sleep(1000);
    }

    public void NewGame()
    {
        Console.WriteLine("The Domino game will be set with default configuration\n\n");

        DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IFaceGenerator), typeof(IntFacesGenerator));
        DependencyContainerRegister.Register.Organizer.SetDefault(typeof(ITokenGenerator), typeof(ClassicTokenGenerator));
        
        Thread.Sleep(1000);
    }

    public void GameOver(Game game)
    {
        Console.WriteLine("The Domino game has finished\n");

        bool thereIsWinner = false;

        foreach(Player player in game.GetAllPlayers())
        {
            if(game.GetPlayerBoard(player).Count == 0)
            {
                Console.WriteLine(player.Id + " has won");
                thereIsWinner = true;
            }
        }

        if(!thereIsWinner)
        {
            Console.WriteLine("Nobody won");
        }

        Thread.Sleep(1000);
    }

    public void Update(Game game)
    {
        if(game.IsDistributed() && game.GetNumberOfMoves() == this._numberOfMoves)
        {
            this._numberOfMoves++;   

            List<Player> players = game.GetAllPlayers();

            foreach(Player player in players)
            {
                Console.WriteLine(player.Id + ":\n" + game.GetBoardString(game.GetPlayerBoard(player)) + "\n");
            }         

            Console.Write("Table: ");

            Move? lastMove = game.GetLastMove();

            if(lastMove == null)
            {
                Console.WriteLine("(Nothing)");
            }
            else
            {
                if(lastMove.Position == Position.Left)
                {
                    Console.WriteLine("(Left move)");
                }
                else if(lastMove.Position == Position.Middle)
                {
                    Console.WriteLine("(First move)");
                }
                else if(lastMove.Position == Position.Right)
                {
                    Console.WriteLine("(Right move)");
                }
                else if(lastMove.Position == Position.Pass)
                {
                    Console.WriteLine("(Pass Turn)");
                }
            }

            Console.WriteLine(game.GetTableString());

            Console.Write("\n");
            Console.Write("\n");
            Console.Write("\n");
            Console.Write("\n");
            Console.Write("\n");
        }

        Thread.Sleep(1000);
    }
}