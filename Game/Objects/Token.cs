struct Token : IComparable
{
    public Tuple<IFace, IFace> Faces;

    public Token(IFace a, IFace b)
    {
        this.Faces = new Tuple<IFace, IFace>(a, b);
    }

    public void Rotate()
    {
        this.Faces = new Tuple<IFace, IFace>(this.Faces.Item2, this.Faces.Item1);
    }

    public int GetTotalValue()
    {
        return Faces.Item1.Value + Faces.Item2.Value;
    }

    public int CompareTo(object? obj)
    {
        if(obj == null || obj.GetType() != this.GetType())
        {
            return -1;
        }

        int a = this.GetTotalValue();

        int b = ((Token)obj).GetTotalValue();

        return a.CompareTo(b);
    }
}