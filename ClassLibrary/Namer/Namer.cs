// Esta clase representa el nombrador de equipos y jugadores
public class Namer
{
    private List<string> _playerNames = new List<string>();
    private List<string> _teamNames = new List<string>();

    private int _playerNamesPosition = 0;
    private int _teamNamesPosition = 0;

    private int _playerNumber = 0;
    private int _teamNumber = 0;

    private List<string> ReadLines(string dir)
    {
        return System.IO.File.ReadLines(dir).ToList();
    }

    // Constructor del Namer, aqui se cargan las bases de datos
    public Namer()
    {
        this._playerNames = this.ReadLines("ClassLibrary\\Namer\\PlayerNameDatabase.txt");
        this._teamNames = this.ReadLines("ClassLibrary\\Namer\\TeamNameDatabase.txt");

        Random random = new Random();

        this._playerNames = this._playerNames.OrderBy(_ => random.Next()).ToList();
        this._teamNames = this._teamNames.OrderBy(_ => random.Next()).ToList();
    }

    // Esta funcion retorna el Id del player solicitado
    public string GetPlayerId()
    {
        string id = "Player" + this._playerNumber;

        this._playerNumber++;

        return id;
    }

    // Esta funcion retorna el nombre del player solicitado
    public string GetPlayerName()
    {
        string name = this._playerNames[this._playerNamesPosition % this._playerNames.Count];

        this._playerNamesPosition++;

        return name;
    }

    // Esta funcion retorna el nombre del jugador humano solicitado
    public string GetHumanName()
    {
        bool Validador(string entry)
        {
            return true;
        }

        Func<string, bool> Func = Validador;

        string name = ((IGraphicInterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicInterface))).GetEntry("GetHumanName", "Insert the name of the human player:", Func);

        return name;
    }

    // Esta funcion retorna el Id del team solicitado
    public string GetTeamId()
    {
        string id = "Team" + this._teamNumber;

        this._teamNumber++;

        return id;
    }

    // Esta funcion retorna el nombre del equipo solicitado
    public string GetTeamName()
    {
        string name = this._teamNames[this._teamNamesPosition % this._teamNames.Count];

        this._teamNamesPosition++;

        return name;
    }
}

public class Names
{
    public static Namer namer = new Namer();
}