interface ITokenDealer : IBaseInterface, ISelector
{
    void Distribute(Box box, List<Board> boards);
}

class ClassicTenTokensDistribution : ITokenDealer
{
    public void Distribute(Box box, List<Board> boards)
    {
        try
        {
            foreach(Board board in boards)
            {
                board.Add(box.Take(10));
            }
        }
        catch (TokenUnavailabilityException)
        {
            throw new TokenDealerUnavailabilityException();
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