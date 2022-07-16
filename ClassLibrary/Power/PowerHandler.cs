// Esta clase representa el manejador de poderes del juego
public class PowerHandler
{
    private List<Power> _powers = new List<Power>();

    // Esta funcion borra todos los poderes
    public void Clear()
    {
        this._powers.Clear();
    }

    // Esta funcion agrega un nuevo poder
    public void AddPower(Power power)
    {
        this._powers.Add(power);
    }

    // Esta funcion agrega nuevos poderes
    public void AddPowers(List<Power> powers)
    {
        foreach(Power power in powers)
        {
            this.AddPower(power);
        }
    }

    // Esta funcion retorna si un power esta activo o no
    public bool GetActivity(string powerId)
    {
        bool activity = false;

        foreach(Power power in this._powers)
        {
            if(powerId == power.Id)
            {
                activity |= power.GetActivity();
            }
        }

        return activity;
    }
}