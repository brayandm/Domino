struct Token
{
    public Tuple<IFace, IFace> Faces;

    public Token(IFace a, IFace b)
    {
        this.Faces = new Tuple<IFace, IFace>(a, b);
    }
}