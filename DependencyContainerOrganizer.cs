using System.Diagnostics;

class DependencyContainerOrganizer
{
    public DependencyContainer Organizer;

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