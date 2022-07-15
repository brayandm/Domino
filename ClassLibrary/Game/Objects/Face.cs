// Esta interface representa una cara
//de una ficha de domino.
public interface IFace : IBaseInterface
{
    // Esto representa el Id de la cara
    string Id {get;}
    // Esto representa el valor de la cara
    int Value {get;}
}

// Esta estructura representa una cara
//numerica de domino, es decir, que esta
//representada por un numero.
public struct IntFace : IFace
{
    public string Id {get;}
    public int Value {get;}

    // Esta funcion crea una cara de valor value 
    //y cuyo id es una cadena de texto con el mismo valor.
    public IntFace(int value)
    {
        this.Id = value.ToString();
        this.Value = value;
    }

    // Esta funcion crea una cara de valor 0 
    //y cuyo id es una cadena de texto vacia.
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

    // Esta funcion crea una cara de valor es 1
    //y cuyo id es la cadena de texto color.
    public ColorFace(string color)
    {
        this.Id = color;
        this.Value = 1;
    }

    // Esta funcion crea una cara de valor 0 
    //y cuyo id es una cadena de texto vacia.
    public ColorFace()
    {
        this.Id = "";
        this.Value = 0;
    }
}