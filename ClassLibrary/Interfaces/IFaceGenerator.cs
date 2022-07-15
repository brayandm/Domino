public interface IFaceGenerator : IBaseInterface
{
    List<IFace> GetFaces();
}

static public class FaceColors
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

static public class FaceInts
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

public class ColorFacesGenerator : IFaceGenerator
{
    public List<IFace> GetFaces() 
    {
        return FaceColors.ColorsToFaces(FaceColors.Colors);
    }

    public List<IFace> GetFaces(List<string> colors)
    {
        return FaceColors.ColorsToFaces(colors);
    }
}

public class IntFacesGenerator : IFaceGenerator
{
    public List<IFace> GetFaces() 
    {
        return FaceInts.IntFaces(10);
    }

    public List<IFace> GetFaces(int n)
    {
        return FaceInts.IntFaces(n);
    }
}