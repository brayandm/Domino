// Esta clase representa un equipo
public class Team
{
    // Este campo representa el ID del equipo
    public string Id;
    // Este campo representa el nombre del equipo
    public string Name;
    // Este campo representa los jugadores del equipo
    public List<Player> Players = new List<Player>();

    // Esta propiedad retorna la cantidad de jugadores en el equipo
    public int Count { get { return this.Players.Count; } }

    // Contructor de la clase
    public Team(string id, string name, List<Player> players)
    {
        this.Id = id;
        this.Name = name;
        this.Players = players;
    }

    // Esta funcion agrega un nuevo jugador al equipo
    public void Add(Player player) 
    {
        if(!this.Players.Contains(player))
        {
            Players.Add(player);
        }
    }

    // Esta funcion remueve un jugador del equipo
    public void Remove(Player player) 
    {
        if(this.Players.Contains(player))
        {
            Players.Remove(player);
        }
    }

    // Esta funcion obtiene un enumerator de los players del equipo
    public IEnumerator<Player> GetEnumerator()
    {
        foreach(Player player in this.Players)
        {
            yield return player;
        }
    }

    // Esta funcion retorna si un jugador pertenece al equipo
    public bool Contains(Player player)
    {
        return this.Players.Contains(player);
    }
}