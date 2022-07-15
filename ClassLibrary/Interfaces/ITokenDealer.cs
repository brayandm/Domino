using System.Diagnostics;

public interface ITokenDealer : IBaseInterface, ISelector
{
    void Distribute(Box box, List<Board> boards);
}

public class VariableTokensDistribution : ITokenDealer
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

        string entry = ((IGraphicInterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicInterface))).GetEntry("GameRule", "Insert the number of tokens to deal to each player (must be greater than zero):", Func);
    
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

public class ClassicTenTokensDistribution : ITokenDealer
{
    public void Distribute(Box box, List<Board> boards)
    {
        foreach(Board board in boards)
        {
            board.Add(box.Take(10));
        }
    }
}

public class ClassicSevenTokensDistribution : ITokenDealer
{
    public void Distribute(Box box, List<Board> boards)
    {
        foreach(Board board in boards)
        {
            board.Add(box.Take(7));
        }
    }
}

public class RandomMaxTenTokensDistribution : ITokenDealer
{
    public void Distribute(Box box, List<Board> boards)
    {
        foreach(Board board in boards)
        {
            board.Add(box.Take((new Random().Next(10)) + 1));
        }
    }
}