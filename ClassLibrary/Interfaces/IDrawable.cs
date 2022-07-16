// Esta interfaz representa si la condicion de robar se cumple
public interface IDrawable : IBaseInterface, ISelector
{
    // Esta funcion representa si la condicion de robar se cumple
    bool IsDrawable(Game game);
}

// Esta clase representa que nunca se roba una ficha
public class ClassicNoDraw : IDrawable
{
    public bool IsDrawable(Game game)
    {
        return false;
    }
}

// Esta clase representa que se roba ficha si el jugador se pasa de turno
public class ClassicPassTurnDraw : IDrawable
{
    public bool IsDrawable(Game game)
    {
        return game.GetNumberOfContiguousPassedTurns() != 0;
    }
}