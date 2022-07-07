interface IGraphicinterface : IBaseInterface
{
    void Main();
    void NewGame();
    void GameOver(Game game);
    void Update(Game game);
}

class ConsoleInterface : IGraphicinterface
{
    private int _time = 1000;
    private int _numberOfMoves = 0;

    private void Clear()
    {
        this._numberOfMoves = 0;
    }

    public void Main()
    {
        Console.WriteLine("Welcome to Domino game\n\n");

        Thread.Sleep(_time);

        while(true)
        {
            Console.WriteLine("Select the speed of the game:\n");
            Console.WriteLine("1 - Very Slow");
            Console.WriteLine("2 - Slow");
            Console.WriteLine("3 - Medium");
            Console.WriteLine("4 - Fast");
            Console.WriteLine("5 - Very Fast");

            Console.WriteLine("\n");

            int selection = Utils.GetIntFromConsoleKeyInfo(Console.ReadKey());

            Console.WriteLine("\n\n");

            if(1 <= selection && selection <= 5)
            {
                if(selection == 1)this._time = 2000;
                if(selection == 2)this._time = 1000;
                if(selection == 3)this._time = 500;
                if(selection == 4)this._time = 100;
                if(selection == 5)this._time = 0;
                break;
            }
            else
            {
                Console.WriteLine("Incorrect selection, repeat it again\n\n");
            }
        }
    }

    public void NewGame()
    {
        this.Clear();

        Console.WriteLine("Do you want to set the default configuration (Y/N)?\n\n");

        ConsoleKeyInfo keyInfo = Console.ReadKey();

        Console.Write("\n\n");

        if(keyInfo.Key == ConsoleKey.Y)
        {
            Console.WriteLine("The Domino game will be set with default configuration\n\n");

            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IBoxGenerator), typeof(ClassicTenBoxGenerator));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IFinalizationRule), typeof(ClassicFinalizationRule));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IGame), typeof(Events.ClassicGame));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IIdJoinable), typeof(ClassicIdJoinable));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IJoinable), typeof(ClassicJoinById));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IOrderPlayerSequence), typeof(ClassicOrderPlayersequence));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IReversePlayerOrder), typeof(NoReverse));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IRoundScorePlayer), typeof(ScorePlayerSumRule));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IRoundScoreTeam), typeof(ScoreTeamSumRule));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(ITeamGenerator), typeof(ClassicFourGreedyTeamsWithOnlyOnePlayer));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(ITokenDealer), typeof(ClassicTenTokensDistribution));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(ITokenValue), typeof(ClassicSumTokenValue));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IRoundWinnerRule), typeof(WinnerRuleMin));
        }
        else
        {
            List<Type> gameInterfaces = DependencyContainerRegister.Register.Organizer.GetSubInterfaces(typeof(ISelector));

            foreach(Type gameInterface in gameInterfaces)
            {
                List<Type> implementations = DependencyContainerRegister.Register.Organizer.GetImplementations(gameInterface);

                while(true)
                {
                    Console.WriteLine("Select the implementation for " + gameInterface + ":\n");

                    for(int i = 0 ; i < implementations.Count ; i++)
                    {
                        Console.WriteLine((i+1) + " - " + implementations[i]);
                    }

                    Console.WriteLine("\n\n");

                    int selection = Utils.GetIntFromConsoleKeyInfo(Console.ReadKey());

                    Console.WriteLine("\n\n");

                    if(1 <= selection && selection <= implementations.Count)
                    {
                        DependencyContainerRegister.Register.Organizer.SetDefault(gameInterface, implementations[selection-1]);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect selection, repeat it again\n\n");
                    }
                }
            }
        }
        
        Thread.Sleep(_time);

        Console.WriteLine("The game will start...\n\n");

        Thread.Sleep(_time*2);
    }

    public void GameOver(Game game)
    {
        Console.WriteLine("The Domino game has finished\n");

        bool thereIsWinner = false;

        foreach(Player player in game.GetAllPlayers())
        {
            if(game.GetPlayerBoard(player).Count == 0)
            {
                Console.WriteLine(player.Id + " has won\n\n\n");
                thereIsWinner = true;
            }
        }

        if(!thereIsWinner)
        {
            Console.WriteLine("Nobody won\n\n\n");
        }

        Thread.Sleep(_time);

        Console.WriteLine("Do you want to play a new Domino game (Y/N)?\n\n");

        ConsoleKeyInfo keyInfo = Console.ReadKey();

        Console.Write("\n\n");

        if(keyInfo.Key != ConsoleKey.Y)
        {
            Environment.Exit(0);
        }
    }

    public void Update(Game game)
    {
        if(game.IsDistributed() && game.GetNumberOfMoves() == this._numberOfMoves)
        {
            this._numberOfMoves++;   

            Move? lastMove = game.GetLastMove();

            List<Player> players = game.GetAllPlayers();

            foreach(Player player in players)
            {
                if(player == game.GetCurrentPlayer() && lastMove != null)
                {
                    Console.WriteLine(player.Id + ": (In Turn)\n" + game.GetBoardString(game.GetPlayerBoard(player)) + "\n");
                }
                else
                {
                    Console.WriteLine(player.Id + ":\n" + game.GetBoardString(game.GetPlayerBoard(player)) + "\n");
                }
            }         

            Console.Write("\nTable: ");

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
            Console.Write("\n");

            Thread.Sleep(_time);
        }
    }
}