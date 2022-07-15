using System.Diagnostics;

public class DependencyContainer
{
    private Dictionary<Type, Type> _default = new Dictionary<Type, Type>();

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

    public Type GetDefault(Type type)
    {
        Debug.Assert(_default.ContainsKey(type));

        return _default[type];
    }

    public List<Type> GetSubInterfaces(Type type)
    {
        return type.Assembly.GetTypes().Where(subType => !subType.Equals(type) && subType.IsInterface && subType.IsAssignableTo(type)).ToList();
    }

    public List<Type> GetImplementations(Type type)
    {
        if(!type.IsInterface)return new List<Type>();

        return type.Assembly.GetTypes().Where(subType => !subType.Equals(type) && subType.IsAssignableTo(type)).ToList();
    }

    public object CreateInstance(Type type, params object?[]? list)
    {
        object? obj = Activator.CreateInstance(type, list);

        Debug.Assert(obj != null); 

        return obj;
    }

    public object CreateInstanceFromDefault(Type type, params object?[]? list)
    {
        object? obj = Activator.CreateInstance(this.GetDefault(type), list);

        Debug.Assert(obj != null); 

        return obj;
    }
}