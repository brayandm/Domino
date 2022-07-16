using System.Diagnostics;

// Esta clase se usa para obtener instancias de las implementaciones
public class DependencyGetter
{
    private Dictionary<Type, dynamic> _instances = new Dictionary<Type, dynamic>();

    // Esta funcion crea las instancias de las implementaciones por default de las interfaces
    public void InitializeAll()
    {
        List<Type> types = DependencyContainerRegister.Register.Organizer.GetSubInterfaces(typeof(IBaseInterface));

        foreach(Type type in types)
        {
            this.Initialize(type);
        }
    }

    // Esta funcion crea la instancia de la implementacion por default de las interfaz
    public void Initialize(Type type)
    {
        if(this._instances.ContainsKey(type))
        {
            return;
        }

        dynamic instance = DependencyContainerRegister.Register.Organizer.CreateInstanceFromDefault(type);

        this._instances.Add(type, instance);
    }

    // Esta funcion asigna la instancia de la implementacion por default de las interfaz
    public void Initialize(Type type, dynamic instance)
    {
        if(this._instances.ContainsKey(type))
        {
            return;
        }

        this._instances.Add(type, instance);
    }

    // Esta funcion retorna la instancia de la implementacion por default de las interfaz
    public dynamic GetInstance(Type type)
    {
        Debug.Assert(_instances.ContainsKey(type));

        return _instances[type];
    }
}