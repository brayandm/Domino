struct Token : IComparable
{
    public List<Power> Powers = new List<Power>();

    public Tuple<IFace, IFace> Faces;

    public Token(IFace a, IFace b)
    {
        this.Faces = new Tuple<IFace, IFace>(a, b);
    }

    public Token(IFace a, IFace b, Power power) : this(a, b)
    {
        this.Powers = new List<Power>(){power};
    } 

    public Token(IFace a, IFace b, List<Power> powers) : this(a, b)
    {
        this.Powers = powers;
    } 

    public new string ToString()
    {
        return "[" + Faces.Item1.Id + ", " + Faces.Item2.Id + "]";
    }

    public bool IsDouble()
    {
        return this.Faces.Item1.Id == this.Faces.Item2.Id;
    }

    public void Rotate()
    {
        this.Faces = new Tuple<IFace, IFace>(this.Faces.Item2, this.Faces.Item1);
    }

    public int GetValue()
    {
        ITokenValue tokenValue = (ITokenValue)DependencyContainerRegister.Getter.GetInstance(typeof(ITokenValue));
        return tokenValue.GetValue(this);
    }

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