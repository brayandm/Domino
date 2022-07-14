interface IFace : IBaseInterface
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

    public IntFace()
    {
        this.Id = "";
        this.Value = 0;
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

    public ColorFace()
    {
        this.Id = "";
        this.Value = 0;
    }
}