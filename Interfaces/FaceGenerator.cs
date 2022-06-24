interface IFaceGenerator : BaseInterface
{
    List<IFace> Faces {get;}
}

static class FaceColors
{
    public static List<string> Colors = new List<string>{"Red", "Green", "Blue", "Yellow", "Orange", "Purple"};

    public static List<IFace> ColorsToFaces(List<string> colors)
    {
        List<IFace> faces = new List<IFace>();

        foreach(string color in colors)
        {
            faces.Add(new ColorFace(color));
        }

        return faces;
    }
}

static class FaceInts
{
    public static List<IFace> IntFaces(int n)
    {
        List<IFace> faces = new List<IFace>();

        for(int i = 0 ; i < n ; i++)
        {
            faces.Add(new IntFace(i));
        }

        return faces;
    }
}

class ColorFacesGenerator : IFaceGenerator
{
    public List<IFace> Faces {get;}

    public ColorFacesGenerator() 
    {
        this.Faces = FaceColors.ColorsToFaces(FaceColors.Colors);
    }

    public ColorFacesGenerator(List<string> colors)
    {
        this.Faces = FaceColors.ColorsToFaces(colors);
    }
}

class IntFacesGenerator : IFaceGenerator
{
    public List<IFace> Faces {get;}

    public IntFacesGenerator() 
    {
        this.Faces = FaceInts.IntFaces(10);
    }

    public IntFacesGenerator(int n)
    {
        this.Faces = FaceInts.IntFaces(n);
    }
}