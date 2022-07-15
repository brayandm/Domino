public class Power
{
    private string _id = "";
    private int _times = 0;
    private List<bool> _actionPattern = new List<bool>();

    private int _position = 0;

    public string Id { get { return this._id; } }

    public Power(string id, int times, List<bool> actionPattern)
    {
        times = Math.Max(times, -1);

        this._id = id;
        this._times = times;
        this._actionPattern = actionPattern;
    }

    public bool GetActivity()
    {
        if(this._times == 0 || this._actionPattern.Count == 0)
        {
            return false;
        }

        bool activity = this._actionPattern[this._position];

        this._position++;

        if(this._position == this._actionPattern.Count)
        {
            this._position = 0;
            this._times = Math.Max(this._times - 1, -1);
        }

        return activity;
    }
}