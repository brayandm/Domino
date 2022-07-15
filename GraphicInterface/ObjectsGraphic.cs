using System.Diagnostics;

public class ObjectsGraphic : IObjectsGraphic
{
    private const int _width = 6;
    private const int _height = 8;
    private const int _dimX = 2;
    private const int _dimY = 7;
    private const int _dimYLite = 2;
    private int _zeroX = 0;
    private int _zeroY = 0;

    public class Point
    {
        public int x = 0;
        public int y = 0;
        public bool rotated = false;
        public bool sense = false;

        public Point(int x, int y, bool rotated = false, bool sense = false)
        {
            this.x = x;
            this.y = y;
            this.rotated = rotated;
            this.sense = sense;
        } 
    }

    private Point GetKthPoint(int k)
    {
        int realX = 0;
        int realY = 0;

        int relativeX = 0;
        int relativeY = 0;

        int dirX, dirY;

        if(k > 0)
        {
            dirX = -1;
            dirY = 1;
        }
        else
        {
            dirX = 1;
            dirY = -1;
        }

        bool rotated = false;
        bool sense = false;

        for(int i = 0 ; i < Math.Abs(k) ; i++)
        {
            relativeY += dirY;

            realY += dirY * _dimY;

            if(Math.Abs(relativeY) > _width)
            {
                if(!rotated)
                {
                    relativeX += dirX;
                    relativeY -= dirY;
                
                    realX += dirX * _dimX;
                    realY -= dirY * (_dimY - _dimYLite);

                    rotated = true;
                    sense = !sense;
                }
                else
                {
                    relativeX += dirX;
                    relativeY -= dirY;
                
                    realX += dirX * _dimX;
                    realY -= dirY * (_dimY + _dimYLite);

                    dirY *= -1;

                    rotated = false;
                }
            }
        }

        if(rotated)
        {
            return new Point(realX, realY, rotated, false);
        }
        else
        {
            return new Point(realX, realY, rotated, sense);
        }
    }

    private void PaintToken(List<List<string>> matrix, Tuple<Token, Point> pointTokens)
    {
        int x = pointTokens.Item2.x - this._zeroX;
        int y = pointTokens.Item2.y - this._zeroY;

        if(!pointTokens.Item2.rotated)
        {
            matrix[x][y-3] = "[";
            if(pointTokens.Item2.sense)
            {
                matrix[x][y-2] = pointTokens.Item1.Faces.Item2.Id;
            }
            else
            {
                matrix[x][y-2] = pointTokens.Item1.Faces.Item1.Id;
            }
            matrix[x][y-1] = " ";
            matrix[x][y] = ":";
            matrix[x][y+1] = " ";
            if(pointTokens.Item2.sense)
            {
                matrix[x][y+2] = pointTokens.Item1.Faces.Item1.Id;
            }
            else
            {
                matrix[x][y+2] = pointTokens.Item1.Faces.Item2.Id;
            }
            matrix[x][y+3] = "]";
        }
        else
        {
            matrix[x-1][y-1] = "[";
            matrix[x-1][y] = pointTokens.Item1.Faces.Item2.Id;
            matrix[x-1][y+1] = "]";
            matrix[x][y-1] = "|";
            matrix[x][y] = ":";
            matrix[x][y+1] = "|";
            matrix[x+1][y-1] = "[";
            matrix[x+1][y] = pointTokens.Item1.Faces.Item1.Id;
            matrix[x+1][y+1] = "]";
        }
    }

    private int GetMinX(List<Tuple<Token, Point>> pointTokens)
    {
        int result = 0;

        foreach(Tuple<Token, Point> pointToken in pointTokens)
        {
            if(!pointToken.Item2.rotated)
            {
                result = Math.Min(result, pointToken.Item2.x);
            }
            else
            {
                result = Math.Min(result, pointToken.Item2.x - 1);
            }
        }

        return result;
    }

    private int GetMaxX(List<Tuple<Token, Point>> pointTokens)
    {
        int result = 0;

        foreach(Tuple<Token, Point> pointToken in pointTokens)
        {
            if(!pointToken.Item2.rotated)
            {
                result = Math.Max(result, pointToken.Item2.x);
            }
            else
            {
                result = Math.Max(result, pointToken.Item2.x + 1);
            }
        }

        return result;
    }

    private string GetStringFromList(List<string> list)
    {
        string result = "";

        foreach(string str in list)
        {
            result += str;
        }

        return result;
    }

    private string GetStringFromMatrix(List<List<string>> matrix)
    {
        string result = "";

        for(int i = 0 ; i < matrix.Count ; i++)
        {
            if(i > 0)
            {
                result += '\n';
            }

            result += this.GetStringFromList(matrix[i]);
        }

        return result;
    }

    private List<List<string>> GetMatrix(int N, int M)
    {
        List<List<string>> matrix = new List<List<string>>();

        for(int i = 0 ; i < N ; i++)
        {
            List<string> list = new List<string>();

            for(int j = 0 ; j < M ; j++)
            {
                list.Add(" ");
            }

            matrix.Add(list);
        }

        return matrix;
    }

    public string GraphicTable(List<Token> tokens, int center)
    {
        if(tokens.Count == 0)
        {
            return "";
        }

        Debug.Assert(0 <= center && center < tokens.Count);

        List<Tuple<Token, Point>> pointTokens = new List<Tuple<Token, Point>>();

        for(int i = 0 ; i < tokens.Count ; i++)
        {
            pointTokens.Add(new Tuple<Token, Point>(tokens[i], this.GetKthPoint(i-center)));
        }

        int minX = Math.Min(this.GetMinX(pointTokens), -_height);
        int maxX = Math.Max(this.GetMaxX(pointTokens), _height);
        
        List<List<string>> matrix = this.GetMatrix(maxX - minX + 1, (_width * 2 + 1) * 7);

        this._zeroX = minX;
        this._zeroY = -(_width * 7 + 3);

        foreach(Tuple<Token, Point> pointToken in pointTokens)
        {
            this.PaintToken(matrix, pointToken);
        }

        return this.GetStringFromMatrix(matrix);
    }

    public string GraphicBoard(List<Token> tokens)
    {
        List<List<string>> matrix = this.GetMatrix(3, tokens.Count*4 - 1);

        for(int i = 0 ; i < tokens.Count ; i++)
        {
            matrix[0][i*4] = "[";
            matrix[0][i*4+1] = tokens[i].Faces.Item1.Id;
            matrix[0][i*4+2] = "]";
            matrix[1][i*4] = "|";
            matrix[1][i*4+1] = ":";
            matrix[1][i*4+2] = "|";
            matrix[2][i*4] = "[";
            matrix[2][i*4+1] = tokens[i].Faces.Item2.Id;
            matrix[2][i*4+2] = "]";
        }

        return this.GetStringFromMatrix(matrix);
    }

    public string GraphicNullableBoard(List<Token?> tokens)
    {
        List<List<string>> matrix = this.GetMatrix(3, tokens.Count*4 - 1);

        for(int i = 0 ; i < tokens.Count ; i++)
        {
            Token? token = tokens[i];

            if(token is Token)
            {
                matrix[0][i*4] = "[";
                matrix[0][i*4+1] = ((Token)token).Faces.Item1.Id;
                matrix[0][i*4+2] = "]";
                matrix[1][i*4] = "|";
                matrix[1][i*4+1] = ":";
                matrix[1][i*4+2] = "|";
                matrix[2][i*4] = "[";
                matrix[2][i*4+1] = ((Token)token).Faces.Item2.Id;
                matrix[2][i*4+2] = "]";
            }
            else
            {
                matrix[0][i*4] = "[";
                matrix[0][i*4+1] = "#";
                matrix[0][i*4+2] = "]";
                matrix[1][i*4] = "|";
                matrix[1][i*4+1] = ":";
                matrix[1][i*4+2] = "|";
                matrix[2][i*4] = "[";
                matrix[2][i*4+1] = "#";
                matrix[2][i*4+2] = "]";
            }
        }

        return this.GetStringFromMatrix(matrix);
    }

    public string GraphicBoardAndPositions(List<Tuple<Token, Position>> tokens)
    {
        List<List<string>> matrix = this.GetMatrix(5, tokens.Count*4 - 1);

        for(int i = 0 ; i < tokens.Count ; i++)
        {
            matrix[0][i*4] = "[";
            matrix[0][i*4+1] = tokens[i].Item1.Faces.Item1.Id;
            matrix[0][i*4+2] = "]";
            matrix[1][i*4] = "|";
            matrix[1][i*4+1] = ":";
            matrix[1][i*4+2] = "|";
            matrix[2][i*4] = "[";
            matrix[2][i*4+1] = tokens[i].Item1.Faces.Item2.Id;
            matrix[2][i*4+2] = "]";
            matrix[3][i*4] = " ";
            matrix[3][i*4+1] = " ";
            matrix[3][i*4+2] = " ";
            matrix[4][i*4] = " ";
            matrix[4][i*4+2] = " ";

            if(tokens[i].Item2 == Position.Left)
            {
                matrix[4][i*4+1] = "L";
            }
            else if(tokens[i].Item2 == Position.Right)
            {
                matrix[4][i*4+1] = "R";
            }
            else if(tokens[i].Item2 == Position.Middle)
            {
                matrix[4][i*4+1] = "M";
            }
        }

        return this.GetStringFromMatrix(matrix);
    }
}