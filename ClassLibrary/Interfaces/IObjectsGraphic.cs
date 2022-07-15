interface IObjectsGraphic : IBaseInterface
{
    string GraphicTable(List<Token> tokens, int center);
    string GraphicBoard(List<Token> tokens);
    string GraphicNullableBoard(List<Token?> tokens);
    string GraphicBoardAndPositions(List<Tuple<Token, Position>> tokens);
}