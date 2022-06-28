using System.Diagnostics;

class DependencyContainerOrganizer
{
    public DependencyContainer organizer;

    public DependencyContainerOrganizer()
    {
        this.organizer = new DependencyContainer();

        List<Type> types = organizer.GetSubInterfaces(typeof(IBaseInterface));

        foreach(Type type in types)
        {
            List<Type> implementations = organizer.GetImplementations(type);

            Debug.Assert(implementations.Count > 0);

            organizer.SetDefault(type, implementations.First());
        }
    }
}