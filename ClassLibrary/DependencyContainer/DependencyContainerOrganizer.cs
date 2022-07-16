using System.Diagnostics;

// Esta clase se usa para organizar automaticamente las implementaciones
//por default de las interfaces
public class DependencyContainerOrganizer
{
    // Este campo representa el registrador de implementaciones
    public DependencyContainer Organizer;

    // Este contructor organiza automaticamente las implementaciones
    //por default de las interfaces
    public DependencyContainerOrganizer()
    {
        this.Organizer = new DependencyContainer();

        List<Type> types = Organizer.GetSubInterfaces(typeof(IBaseInterface));

        foreach(Type type in types)
        {
            List<Type> implementations = Organizer.GetImplementations(type);

            Debug.Assert(implementations.Count > 0);

            Organizer.SetDefault(type, implementations.First());
        }
    }
}