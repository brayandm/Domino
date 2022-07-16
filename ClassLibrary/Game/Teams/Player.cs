// Esta clase representa un jugador
public class Player
{
    // Esta campo representa el ID del jugador
    public string Id;

    // Este campo representa el nombre del jugador
    public string Name;

    // Este campo representa la estrategia del jugador
    public IStrategy Strategy;

    // Este es el contructor de la clase player
    public Player(string id, string name, IStrategy strategy)
    {
        this.Id = id;
        this.Name = name;
        this.Strategy = strategy;
    }
}