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

    public int CompareTo(object? obj)
    {
        if(obj == null || obj.GetType() != this.GetType())
        {
            return -1;
        }

        int a = this.Faces.Item1.Value + this.Faces.Item2.Value;

        int b = ((Token)obj).Faces.Item1.Value + ((Token)obj).Faces.Item2.Value;

        return a.CompareTo(b);
    }
}