using System.Diagnostics;

// Esta clase se utiliza para registrar implementaciones y crear instancias de ellas
public class DependencyContainer
{
    private Dictionary<Type, Type> _default = new Dictionary<Type, Type>();

    // Esta funcion asigna un tipo por default de otro
    public void SetDefault(Type typeA, Type typeB)
    {
        if(!_default.ContainsKey(typeA))
        {
            _default.Add(typeA, typeB);
        }
        else
        {
            _default[typeA] = typeB;
        }
    }

    // Esta funcion retorna su tipo por dafault
    public Type GetDefault(Type type)
    {
        Debug.Assert(_default.ContainsKey(type));

        return _default[type];
    }

    // Esta funcion retorna todas las interfaces hijos de un tipo
    public List<Type> GetSubInterfaces(Type type)
    {
        return type.Assembly.GetTypes().Where(subType => !subType.Equals(type) && subType.IsInterface && subType.IsAssignableTo(type)).ToList();
    }

    // Esta funcion retorna las implementaciones de una interfaz
    public List<Type> GetImplementations(Type type)
    {
        if(!type.IsInterface)return new List<Type>();

        return type.Assembly.GetTypes().Where(subType => !subType.Equals(type) && subType.IsAssignableTo(type)).ToList();
    }

    // Esta funcion retorna una instancia de un tipo
    public object CreateInstance(Type type, params object?[]? list)
    {
        object? obj = Activator.CreateInstance(type, list);

        Debug.Assert(obj != null); 

        return obj;
    }

    // Esta funcion retorna una instancia del default de un tipo
    public object CreateInstanceFromDefault(Type type, params object?[]? list)
    {
        object? obj = Activator.CreateInstance(this.GetDefault(type), list);

        Debug.Assert(obj != null); 

        return obj;
    }
}