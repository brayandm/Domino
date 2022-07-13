class Move
{
    public readonly Player Player;
    public readonly ProtectedToken? Token;
    public readonly Position Position;

    public Move(Player player, ProtectedToken? token, Position position)
    {
        this.Player = player;
        this.Token = token;
        this.Position = position;
    }
}