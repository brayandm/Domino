using System.Diagnostics;

class DependencyContainer
{
    private Dictionary<Type, Type> Default = new Dictionary<Type, Type>();

    public void SetDefault(Type typeA, Type typeB)
    {
        if(!Default.ContainsKey(typeA))
        {
            Default.Add(typeA, typeB);
        }
        else
        {
            Default[typeA] = typeB;
        }
    }

    public Type GetDefault(Type type)
    {
        Debug.Assert(Default.ContainsKey(type));

        return Default[type];
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

    public object GetInstance(Type type, params object?[]? list)
    {
        object? obj = Activator.CreateInstance(type, list);

        Debug.Assert(obj != null); 

        return obj;
    }
}