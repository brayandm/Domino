public class Program
{ 
    public static void Main()
    {
        DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IGraphicInterface), typeof(ConsoleInterface));
        DependencyContainerRegister.Register.Organizer.SetDefault(typeof(IObjectsGraphic), typeof(ObjectsGraphic));

        DependencyContainerRegister.Getter.Initialize(typeof(IGraphicInterface));
        DependencyContainerRegister.Getter.Initialize(typeof(IObjectsGraphic));
        
        GameLogic gameLogic = new GameLogic();
    }
}