struct Token : IComparable
{
    public Power? Power = null;

    public Tuple<IFace, IFace> Faces;

    public Token(IFace a, IFace b)
    {
        this.Faces = new Tuple<IFace, IFace>(a, b);
    }

    public Token(IFace a, IFace b, Power power) : this(a, b)
    {
        this.Power = power;
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
        ITokenValue tokenValue = (ITokenValue)DependencyContainerRegister.Register.Organizer.CreateInstanceFromDefault(typeof(ITokenValue));
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