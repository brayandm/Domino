using System.Diagnostics;

class DependencyGetter
{
    private Dictionary<Type, dynamic> _instances = new Dictionary<Type, dynamic>();

    private bool _isInitialized = false;

    public void Initialize()
    {
        if(this._isInitialized)
        {
            return;
        }

        this._isInitialized = true;

        List<Type> types = DependencyContainerRegister.Register.Organizer.GetSubInterfaces(typeof(IBaseInterface));

        foreach(Type type in types)
        {
            dynamic instance = DependencyContainerRegister.Register.Organizer.CreateInstanceFromDefault(type);

            _instances.Add(type, instance);
        }
    }

    public dynamic GetInstance(Type type)
    {
        Debug.Assert(_instances.ContainsKey(type));

        return _instances[type];
    }
}