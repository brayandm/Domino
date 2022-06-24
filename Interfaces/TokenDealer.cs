interface ITokenDealer : IBaseInterface
{
    void Distribute(Box box, List<Board> boards);
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