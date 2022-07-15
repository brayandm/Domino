// Esta clase representa un movimiento del juego
public class Move
{
    // Este campo representa el jugador que hizo el movimiento
    public readonly Player Player;
    // Este campo representa el token que fue jugado
    public readonly ProtectedToken? Token;
    // Este campo representa el tipo de movimiento hecho
    public readonly Position Position;

    // Este es el contructor del movimiento
    public Move(Player player, ProtectedToken? token, Position position)
    {
        this.Player = player;
        this.Token = token;
        this.Position = position;
    }
}