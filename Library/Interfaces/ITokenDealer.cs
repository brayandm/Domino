using System.Diagnostics;

interface ITokenDealer : IBaseInterface, ISelector
{
    void Distribute(Box box, List<Board> boards);
}

class VariableTokensDistribution : ITokenDealer
{
    private int _tokensToDeal = 0;

    public VariableTokensDistribution()
    {
        while(true)
        {
            Console.WriteLine("Insert the number of tokens to deal to each player (must be greater than zero):\n\n\n\n");
            
            string? entry = Console.ReadLine();

            if(entry is string)
            {
                try
                {
                    this._tokensToDeal = int.Parse(entry);
                    
                    if(this._tokensToDeal <= 0)
                    {
                        Console.WriteLine("\n\n\n\nThe inserted number is incorrect, repeat it again\n\n");
                        
                        continue;
                    }

                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("\n\n\n\nThe inserted number is incorrect, repeat it again\n\n");
                }
            }
            else
            {
                Console.WriteLine("\n\n\n\nThe inserted number is incorrect, repeat it again\n\n");
            }
        }

        Console.WriteLine("\n\n\n\n");
    }

    public void Distribute(Box box, List<Board> boards)
    {
        foreach(Board board in boards)
        {
            board.Add(box.Take(this._tokensToDeal));
        }
    }
}

class ClassicTenTokensDistribution : ITokenDealer
{
    public void Distribute(Box box, List<Board> boards)
    {
        foreach(Board board in boards)
        {
            board.Add(box.Take(10));
        }
    }
}

class ClassicSevenTokensDistribution : ITokenDealer
{
    public void Distribute(Box box, List<Board> boards)
    {
        foreach(Board board in boards)
        {
            board.Add(box.Take(7));
        }
    }
}

class RandomMaxTenTokensDistribution : ITokenDealer
{
    public void Distribute(Box box, List<Board> boards)
    {
        foreach(Board board in boards)
        {
            board.Add(box.Take((new Random().Next(10)) + 1));
        }
    }
}