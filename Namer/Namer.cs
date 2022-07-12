class Namer
{
    List<string> _playerNames = new List<string>();
    List<string> _teamNames = new List<string>();

    private int _playerNamesPosition = 0;
    private int _teamNamesPosition = 0;

    private int _playerNumber = 0;
    private int _teamNumber = 0;

    List<string> ReadLines(string dir)
    {
        return System.IO.File.ReadLines(dir).ToList();
    }

    public Namer()
    {
        this._playerNames = this.ReadLines("Namer\\PlayerNameDatabase.txt");
        this._teamNames = this.ReadLines("Namer\\TeamNameDatabase.txt");

        Random random = new Random();

        this._playerNames = this._playerNames.OrderBy(_ => random.Next()).ToList();
        this._teamNames = this._teamNames.OrderBy(_ => random.Next()).ToList();
    }

    public string GetPlayerId()
    {
        string id = "Player" + this._playerNumber;

        this._playerNumber++;

        return id;
    }

    public string GetPlayerName()
    {
        string name = this._playerNames[this._playerNamesPosition % this._playerNames.Count];

        this._playerNamesPosition++;

        return name;
    }

    public string GetTeamId()
    {
        string id = "Team" + this._teamNumber;

        this._teamNumber++;

        return id;
    }

    public string GetTeamName()
    {
        string name = this._teamNames[this._teamNamesPosition % this._teamNames.Count];

        this._teamNamesPosition++;

        return name;
    }
}

class Names
{
    public static Namer namer = new Namer();
}