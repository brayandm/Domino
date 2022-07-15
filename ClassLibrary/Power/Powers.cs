public class PassNextPlayerTurnPower : Power
{
    public PassNextPlayerTurnPower() : base("PassNextPlayerTurnPower", 1, new List<bool>(){true}) {}
}

public class PlayAgainCurrentPlayerPower : Power
{
    public PlayAgainCurrentPlayerPower() : base("PlayAgainCurrentPlayerPower", 1, new List<bool>(){true}) {}
}

public class EndRoundPower : Power
{
    public EndRoundPower() : base("EndRoundPower", 1, new List<bool>(){true}) {}
}

public class EndGamePower : Power
{
    public EndGamePower() : base("EndGamePower", -1, new List<bool>(){true}) {}
}