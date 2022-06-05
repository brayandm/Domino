interface IFace
{
    string Id {get;}
    int Value {get;}
}

struct IntFace : IFace
{
    public string Id {get;}
    public int Value {get;}

    public IntFace(int value)
    {
        this.Id = value.ToString();
        this.Value = value;
    }
}

struct ColorFace : IFace
{
    public string Id {get;}
    public int Value {get;}

    public ColorFace(string color)
    {
        this.Id = color;
        this.Value = 1;
    }
}