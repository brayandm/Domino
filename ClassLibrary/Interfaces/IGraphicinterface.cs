// Esta interfaz representa la interfaz grafica
public interface IGraphicInterface : IGraphics
{
    // Esta funcion grafica la tabla
    string GraphicTable(List<Token> tokens, int center);
    // Esta funcion grafica el board
    string GraphicBoard(List<Token> tokens);
    // Esta funcion grafica el board con algunos tokens no visibles
    string GraphicNullableBoard(List<Token?> tokens);
    // Esta funcion grafica el board con donde se hace el movimiento
    string GraphicBoardAndPositions(List<Tuple<Token, Position>> tokens);
    // Esta funcion lee una entrada
    string GetEntry(string id, string message, Func<string,bool> validator);
    // Esta funcion envia un mensaje
    void SendMessage(string id, string message);
    // Esta funcion ejecuta una accion
    void Action(string id);
    // Esta funcion es el menu principal
    void Main();
    // Esta funcion empieza un nuevo juego
    void NewGame();
    // Esta funcion empieza un nuevo torneo
    void NewTournament();
    // Esta funcion finaliza el juego
    void GameOver(Game game);
    // Esta funcion finaliza el torneo
    void TournamentOver(Tournament game);
    // Esta funcion updatea el juego
    void UpdateGame(Game game);
    // Esta funcion updatea el torneo
    void UpdateTournament(Tournament game);
}