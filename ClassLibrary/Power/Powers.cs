// Esta clase representa el power de que se pase el proximo jugador
public class PassNextPlayerTurnPower : Power
{
    public PassNextPlayerTurnPower() : base("PassNextPlayerTurnPower", 1, new List<bool>(){true}) {}
}

// Esta clase representa el power de que vuelvas a jugar tu mismo turno
public class PlayAgainCurrentPlayerPower : Power
{
    public PlayAgainCurrentPlayerPower() : base("PlayAgainCurrentPlayerPower", 1, new List<bool>(){true}) {}
}

// Esta clase representa el final de una ronda
public class EndRoundPower : Power
{
    public EndRoundPower() : base("EndRoundPower", 1, new List<bool>(){true}) {}
}

// Esta clase representa el final del juego
public class EndGamePower : Power
{
    public EndGamePower() : base("EndGamePower", -1, new List<bool>(){true}) {}
}