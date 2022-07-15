public interface IGraphicInterface : IGraphics
{
    string GraphicTable(List<Token> tokens, int center);
    string GraphicBoard(List<Token> tokens);
    string GraphicNullableBoard(List<Token?> tokens);
    string GraphicBoardAndPositions(List<Tuple<Token, Position>> tokens);
    string GetEntry(string id, string message, Func<string,bool> validator);
    void SendMessage(string id, string message);
    void Action(string id);
    void Main();
    void NewGame();
    void NewTournament();
    void GameOver(Game game);
    void TournamentOver(Tournament game);
    void UpdateGame(Game game);
    void UpdateTournament(Tournament game);
}