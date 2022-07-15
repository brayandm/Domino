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
        bool Validador(string entry)
        {
            try
            {
                int value = int.Parse(entry);
                
                return value > 0;
            }
            catch
            {
                return false;
            }
        }

        Func<string, bool> Func = Validador;

        string entry = Graphics.graphicinterface.GetEntry("GameRule", "Insert the number of tokens to deal to each player (must be greater than zero):", Func);
    
        this._tokensToDeal = int.Parse(entry);
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