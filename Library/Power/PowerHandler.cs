class PowerHandler
{
    private List<Power> _powers = new List<Power>();

    public void AddPower(Power power)
    {
        this._powers.Add(power);
    }

    public void AddPowers(List<Power> powers)
    {
        foreach(Power power in powers)
        {
            this.AddPower(power);
        }
    }

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