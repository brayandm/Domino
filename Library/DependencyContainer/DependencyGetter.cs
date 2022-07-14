using System.Diagnostics;

class DependencyGetter
{
    private Dictionary<Type, dynamic> _instances = new Dictionary<Type, dynamic>();

    public void InitializeAll()
    {
        List<Type> types = DependencyContainerRegister.Register.Organizer.GetSubInterfaces(typeof(IBaseInterface));

        foreach(Type type in types)
        {
            this.Initialize(type);
        }
    }

    public void Initialize(Type type)
    {
        if(this._instances.ContainsKey(type))
        {
            return;
        }

        dynamic instance = DependencyContainerRegister.Register.Organizer.CreateInstance(type);

        this._instances.Add(type, instance);
    }

    public dynamic GetInstance(Type type)
    {
        Debug.Assert(_instances.ContainsKey(type));

        return _instances[type];
    }
}