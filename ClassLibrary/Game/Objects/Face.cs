// Esta interface representa una cara
//de una ficha de domino.
public interface IFace : IBaseInterface
{
    string Id {get;}
    int Value {get;}
}

// Esta estructura representa una cara
//numerica de domino, es decir, que esta
//representada por un numero.
public struct IntFace : IFace
{
    public string Id {get;}
    public int Value {get;}

    public IntFace(int value)
    {
        this.Id = value.ToString();
        this.Value = value;
    }

    public IntFace()
    {
        this.Id = "";
        this.Value = 0;
    }
}

// Esta estructura representa una cara
//coloreada de domino, es decir, que esta
//representada por un color.
public struct ColorFace : IFace
{
    public string Id {get;}
    public int Value {get;}

    public ColorFace(string color)
    {
        this.Id = color;
        this.Value = 1;
    }

    public ColorFace()
    {
        this.Id = "";
        this.Value = 0;
    }
}