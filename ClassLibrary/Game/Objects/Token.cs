// Esta clase representa una ficha del juego
public struct Token : IComparable
{
    public List<Power> Powers = new List<Power>();

    public Tuple<IFace, IFace> Faces;

    // Esta funcion crea un Token de la forma 
    //          [a, b]
    public Token(IFace a, IFace b)
    {
        this.Faces = new Tuple<IFace, IFace>(a, b);
    }

    // Esta funcion crea un Token de la forma 
    //          [a, b]
    //con la etiqueta power.
    public Token(IFace a, IFace b, Power power) : this(a, b)
    {
        this.Powers = new List<Power>(){power};
    } 

    // Esta funcion crea un Token de la forma 
    //          [a, b]
    //con las etiquetas powers.
    public Token(IFace a, IFace b, List<Power> powers) : this(a, b)
    {
        this.Powers = powers;
    } 

    public new string ToString()
    {
        return "[" + Faces.Item1.Id + ", " + Faces.Item2.Id + "]";
    }

    // Esta funcion comprueba si la ficha es doble.
    //Es decir, si ambas caras son iguales.
    public bool IsDouble()
    {
        return this.Faces.Item1.Id == this.Faces.Item2.Id;
    }

    // Esta funcion intercambia las caras de la ficha.
    public void Rotate()
    {
        this.Faces = new Tuple<IFace, IFace>(this.Faces.Item2, this.Faces.Item1);
    }

    // Esta funcion devuelve el valor de la ficha.
    public int GetValue()
    {
        ITokenValue tokenValue = (ITokenValue)DependencyContainerRegister.Getter.GetInstance(typeof(ITokenValue));
        return tokenValue.GetValue(this);
    }

    // Esta funcion compara este objeto con obj
    //mediante la funcion GetValue().
    public int CompareTo(object? obj)
    {
        if(obj == null || obj.GetType() != this.GetType())
        {
            return -1;
        }

        int a = this.GetValue();

        int b = ((Token)obj).GetValue();

        return a.CompareTo(b);
    }
}