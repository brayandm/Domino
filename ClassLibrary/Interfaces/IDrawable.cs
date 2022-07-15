public interface IDrawable : IBaseInterface, ISelector
{
    bool IsDrawable(Game game);
}

class ClassicNoDraw : IDrawable
{
    public bool IsDrawable(Game game)
    {
        return false;
    }
}

class ClassicPassTurnDraw : IDrawable
{
    public bool IsDrawable(Game game)
    {
        return game.GetNumberOfContiguousPassedTurns() != 0;
    }
}