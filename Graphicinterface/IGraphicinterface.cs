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
    private int _numberOfRounds = 0;
    private bool _roundEnded = false;
    private ObjectsGraphic _objectsGraphic = new ObjectsGraphic();
    private bool _consoleClearable = false;   

    private void Clear()
    {
        this._numberOfMoves = 0;
        this._numberOfRounds = 0;
        this._roundEnded = false;
    }

    private void ConsoleClear()
    {
        if(this._consoleClearable)
        {
            Console.Clear();
        }
    }

    public void Main()
    {
        this.ConsoleClear();

        Console.WriteLine("Welcome to Domino game\n\n");

        Thread.Sleep(_time);

        while(true)
        {
            this.ConsoleClear();

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
                this.ConsoleClear();

                Console.WriteLine("Incorrect selection, repeat it again\n\n");

                Thread.Sleep(_time);
            }
        }
    }

    public void NewGame()
    {
        this.ConsoleClear();

        this.Clear();

        Console.WriteLine("Do you want to set the default configuration (Y/N)?\n\n");

        ConsoleKeyInfo keyInfo = Console.ReadKey();

        Console.Write("\n\n");

        if(keyInfo.Key == ConsoleKey.Y)
        {
            this.ConsoleClear();

            Console.WriteLine("The Domino game will be set with default configuration\n\n");

            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IBoxGenerator), typeof(ClassicTenBoxGenerator));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IDrawable), typeof(ClassicPassTurnDraw));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IGame), typeof(Events.ClassicGame));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IGameFinalizable), typeof(ThreeRoundGameFinalizable));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IIdJoinable), typeof(ClassicIdJoinable));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IJoinable), typeof(ClassicJoinById));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IOrderPlayerSequence), typeof(ClassicOrderPlayersequence));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IReversePlayerOrder), typeof(NoReverse));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IRoundFinalizationRule), typeof(ClassicFinalizationRule));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IRoundGame), typeof(Events.ClassicRoundGame));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IRoundScorePlayer), typeof(RoundScorePlayerSumRule));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IRoundScoreTeam), typeof(RoundScoreTeamSumRule));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IRoundWinnerRule), typeof(RoundWinnerRuleMin));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IScoreTeam), typeof(ScoreTeamSumRule));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(ITeamGenerator), typeof(ClassicTwoGreedyTeams));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(ITokenDealer), typeof(ClassicTenTokensDistribution));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(ITokenValue), typeof(ClassicSumTokenValue));
            DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IWinnerRule), typeof(WinnerRuleMin));
        }
        else
        {
            List<Type> gameInterfaces = DependencyContainerRegister.Register.Organizer.GetSubInterfaces(typeof(ISelector));

            foreach(Type gameInterface in gameInterfaces)
            {
                List<Type> implementations = DependencyContainerRegister.Register.Organizer.GetImplementations(gameInterface);

                while(true)
                {
                    this.ConsoleClear();

                    Console.WriteLine("Select the implementation for " + gameInterface + ":\n");

                    Console.WriteLine(0 + " - " + "Default");

                    for(int i = 0 ; i < implementations.Count ; i++)
                    {
                        Console.WriteLine((i+1) + " - " + implementations[i]);
                    }

                    Console.WriteLine("\n\n");

                    int selection = Utils.GetIntFromConsoleKeyInfo(Console.ReadKey());

                    Console.WriteLine("\n\n");

                    if(selection == 0)
                    {
                        break;
                    }
                    else if(1 <= selection && selection <= implementations.Count)
                    {
                        DependencyContainerRegister.Register.Organizer.SetDefault(gameInterface, implementations[selection-1]);
                        break;
                    }
                    else
                    {
                        this.ConsoleClear();

                        Console.WriteLine("Incorrect selection, repeat it again\n\n");

                        Thread.Sleep(_time);
                    }
                }
            }
        }
        
        this.ConsoleClear();

        Console.WriteLine("The game will start...\n\n");

        Thread.Sleep(_time*2);
    }

    public void GameOver(Game game)
    {
        this.ConsoleClear();

        Console.WriteLine("The Domino game has finished\n");

        List<Team> winners = game.GetWinnersAllRound();

        for(int i = 0 ; i < winners.Count ; i++)
        {
            if(0 < i && i + 1 < winners.Count)
            {
                Console.Write(", ");
            }
            else if(0 < i && i + 1 == winners.Count)
            {
                Console.Write(" and ");
            }
            Console.Write(winners[i].Id);
        }

        if(winners.Count == 0)
        {
            Console.WriteLine("Nobody won\n\n\n\n\n");
        }
        else if(winners.Count == 1)
        {
            Console.WriteLine(" has won with " + game.GetTeamAllRoundScore(winners.Last()) + " points\n\n\n\n\n");
        }
        else if(winners.Count < game.GetAllTeams().Count)
        {
            Console.WriteLine(" have won with " + game.GetTeamAllRoundScore(winners.Last()) + " points\n\n\n\n\n");
        }
        else if(winners.Count == game.GetAllTeams().Count)
        {
            Console.WriteLine(" have tied with " + game.GetTeamAllRoundScore(winners.Last()) + " points\n\n\n\n\n");
        }

        Console.WriteLine("Scoreboard final:\n");

        foreach(Team team in game.GetAllTeams())
        {
            Console.WriteLine(team.Id + " has " + game.GetTeamAllRoundScore(team) + " points.");
        }
        Console.Write("\n\n\n\n\n");
        Console.WriteLine("Press any key to continue...");
        Console.Write("\n\n\n\n\n");
        Console.ReadKey();

        Thread.Sleep(_time);

        this.ConsoleClear();

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
        if(!this._roundEnded && game.IsCurrentRoundEnded())
        {
            this.ConsoleClear();

            this._roundEnded = true;

            Console.WriteLine("The round " + this._numberOfRounds + " has finished\n");

            List<Team> winners = game.GetRoundWinners();

            for(int i = 0 ; i < winners.Count ; i++)
            {
                if(0 < i && i + 1 < winners.Count)
                {
                    Console.Write(", ");
                }
                else if(0 < i && i + 1 == winners.Count)
                {
                    Console.Write(" and ");
                }
                Console.Write(winners[i].Id);
            }

            if(winners.Count == 0)
            {
                Console.WriteLine("Nobody won\n\n\n\n\n");
            }
            else if(winners.Count == 1)
            {
                Console.WriteLine(" has won with " + game.GetRoundTeamScore(winners.Last()) + " points\n\n\n\n\n");
            }
            else if(winners.Count < game.GetAllTeams().Count)
            {
                Console.WriteLine(" have won with " + game.GetRoundTeamScore(winners.Last()) + " points\n\n\n\n\n");
            }
            else if(winners.Count == game.GetAllTeams().Count)
            {
                Console.WriteLine(" have tied with " + game.GetRoundTeamScore(winners.Last()) + " points\n\n\n\n\n");
            }

            Console.WriteLine("Scoreboard updated:\n");

            foreach(Team team in game.GetAllTeams())
            {
                Console.WriteLine(team.Id + " has " + game.GetTeamAllRoundScore(team) + " points.");
            }
            Console.Write("\n\n\n\n\n");
            Console.WriteLine("Press any key to continue...");
            Console.Write("\n\n\n\n\n");
            Console.ReadKey();
        }
        else if(game.GetNumberOfRounds() > this._numberOfRounds)
        {
            this.ConsoleClear();

            this._numberOfRounds = game.GetNumberOfRounds();
            this._numberOfMoves = 0;
            this._roundEnded = false;
        }
        else if(game.IsDistributed() && game.GetNumberOfMoves() == this._numberOfMoves)
        {
            this.ConsoleClear();

            this._numberOfMoves++;   

            Move? lastMove = game.GetLastMove();

            List<Player> players = game.GetAllPlayers();

            foreach(Player player in players)
            {
                if(player == game.GetCurrentPlayer() && lastMove != null)
                {
                    Console.WriteLine(player.Id + ": (In Turn)\n\n" + this._objectsGraphic.GraphicBoard(game.GetBoardTokens(game.GetPlayerBoard(player))) + "\n");
                }
                else
                {
                    Console.WriteLine(player.Id + ":\n\n" + this._objectsGraphic.GraphicBoard(game.GetBoardTokens(game.GetPlayerBoard(player))) + "\n");
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
                else if(lastMove.Position == Position.Draw)
                {
                    Console.WriteLine("(Drawn Token)");
                }
            }

            Console.WriteLine("\n" + this._objectsGraphic.GraphicTable(game.GetTabletokens(), game.GetPositionMiddle()));

            Thread.Sleep(_time);

            Console.Write("\n");
            Console.Write("\n");
            Console.Write("\n");
            Console.Write("\n");
            Console.Write("\n");
            Console.Write("\n");
        }
    }
}