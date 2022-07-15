class PassNextPlayerTurnPower : Power
{
    public PassNextPlayerTurnPower() : base("PassNextPlayerTurnPower", 1, new List<bool>(){true}) {}
}

class PlayAgainCurrentPlayerPower : Power
{
    public PlayAgainCurrentPlayerPower() : base("PlayAgainCurrentPlayerPower", 1, new List<bool>(){true}) {}
}

class EndRoundPower : Power
{
    public EndRoundPower() : base("EndRoundPower", 1, new List<bool>(){true}) {}
}

class EndGamePower : Power
{
    public EndGamePower() : base("EndGamePower", -1, new List<bool>(){true}) {}
}