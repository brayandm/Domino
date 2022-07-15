public interface IGraphicInterface : IGraphics
{
    IObjectsGraphic ObjectsGraphic {get;}
    void Main();
    void NewGame();
    void NewTournament();
    void GameOver(Game game);
    void TournamentOver(Tournament game);
    void UpdateGame(Game game);
    void UpdateTournament(Tournament game);
    string GetEntry(string id, string message, Func<string,bool> validator);
    void SendMessage(string id, string message);
}