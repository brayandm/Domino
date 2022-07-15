interface IStrategy : IBaseInterface
{
    int ChooseTokenIndex(List<Tuple<Token, Position>> playableTokensAndPositions, Player player, List<Team> teams, Dictionary<Player, List<Token?>> playerBoard, List<Token> tableTokens, int center);
}

class HumanSelection : IStrategy
{
    public int ChooseTokenIndex(List<Tuple<Token, Position>> playableTokensAndPositions, Player player, List<Team> teams, Dictionary<Player, List<Token?>> playerBoard, List<Token> tableTokens, int center)
    {
        if(playableTokensAndPositions.Count == 0)
        {
            ((IGraphicinterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicinterface))).SendMessage("HumanSelection", "There is not token to play");
            
            return -1;
        }

        bool Validador(string entry)
        {
            try
            {
                int value = int.Parse(entry);
                
                return 1 <= value && value <= playableTokensAndPositions.Count;
            }
            catch
            {
                return false;
            }
        }

        Func<string, bool> Func = Validador;

        ((IGraphicinterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicinterface))).SendMessage("ShowPlayableTokens", player.Name + " Board:\n\n");
        ((IGraphicinterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicinterface))).SendMessage("ShowPlayableTokens", ((IGraphicinterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicinterface))).ObjectsGraphic.GraphicNullableBoard(playerBoard[player]));
        ((IGraphicinterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicinterface))).SendMessage("ShowPlayableTokens", "\n\n");
        ((IGraphicinterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicinterface))).SendMessage("ShowPlayableTokens", "Table:\n\n");
        ((IGraphicinterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicinterface))).SendMessage("ShowPlayableTokens", ((IGraphicinterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicinterface))).ObjectsGraphic.GraphicTable(tableTokens, center));
        ((IGraphicinterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicinterface))).SendMessage("ShowPlayableTokens", "\n\n");
        ((IGraphicinterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicinterface))).SendMessage("ShowPlayableTokens", "Playable Tokens:\n\n");
        ((IGraphicinterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicinterface))).SendMessage("ShowPlayableTokens", ((IGraphicinterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicinterface))).ObjectsGraphic.GraphicBoardAndPositions(playableTokensAndPositions));
        ((IGraphicinterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicinterface))).SendMessage("ShowPlayableTokens", "\n\n");

        string entry = ((IGraphicinterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicinterface))).GetEntry("HumanSelection", "Insert the number of the token to play (must be in range [1, " + playableTokensAndPositions.Count + "])", Func);
    
        return int.Parse(entry) - 1;
    }
}

class GreedyStrategy : IStrategy
{
    public int ChooseTokenIndex(List<Tuple<Token, Position>> playableTokensAndPositions, Player player, List<Team> teams, Dictionary<Player, List<Token?>> playerBoard, List<Token> tableTokens, int center)
    {
        List<Token> playableTokens = new List<Token>();

        foreach(Tuple<Token, Position> playableToken in playableTokensAndPositions)
        {
            playableTokens.Add(playableToken.Item1);
        }

        if(playableTokens.Count == 0)return -1;
     
        Token token = playableTokens[0];
        int result = 0;

        for(int i = 0 ; i < playableTokens.Count ; i++)
        {
            if(token.CompareTo(playableTokens[i]) < 0)
            {
                token = playableTokens[i];
                result = i;
            }
        }

        return result;
    }
}

class RandomStrategy : IStrategy
{
    public int ChooseTokenIndex(List<Tuple<Token, Position>> playableTokensAndPositions, Player player, List<Team> teams, Dictionary<Player, List<Token?>> playerBoard, List<Token> tableTokens, int center)
    {
        List<Token> playableTokens = new List<Token>();

        foreach(Tuple<Token, Position> playableToken in playableTokensAndPositions)
        {
            playableTokens.Add(playableToken.Item1);
        }

        if(playableTokens.Count == 0)return -1;

        return new Random().Next(playableTokens.Count);
    }
}

class FrequencyStrategy : IStrategy
{
    public int ChooseTokenIndex(List<Tuple<Token, Position>> playableTokensAndPositions, Player player, List<Team> teams, Dictionary<Player, List<Token?>> playerBoard, List<Token> tableTokens, int center)
    {
        List<Token> playableTokens = new List<Token>();

        foreach(Tuple<Token, Position> playableToken in playableTokensAndPositions)
        {
            playableTokens.Add(playableToken.Item1);
        }

        if(playableTokens.Count == 0)return -1;

        Dictionary<string, int> idFrequency = new Dictionary<string, int>();

        foreach(Token token in playableTokens)
        {
            if(!idFrequency.ContainsKey(token.Faces.Item1.Id))
            {
                idFrequency.Add(token.Faces.Item1.Id, 0);
            }

            if(!idFrequency.ContainsKey(token.Faces.Item2.Id))
            {
                idFrequency.Add(token.Faces.Item2.Id, 0);
            }

            if(token.Faces.Item1.Id != token.Faces.Item2.Id)
            {
                idFrequency[token.Faces.Item1.Id]++;
                idFrequency[token.Faces.Item2.Id]++;
            }
            else
            {
                idFrequency[token.Faces.Item1.Id]++;
            }
        }

        string mostFrequencyId = "";
        int frequency = 0;

        foreach(var id in idFrequency)
        {
            if(id.Value > frequency)
            {
                mostFrequencyId = id.Key;
                frequency = id.Value;
            }
        }

        List<bool> tokensToSelect = new List<bool>();

        foreach(Token token in playableTokens)
        {
            tokensToSelect.Add(token.Faces.Item1.Id == mostFrequencyId || token.Faces.Item2.Id == mostFrequencyId);
        }

        Token tokenToPlay = new Token();
        int result = 0;

        for(int i = 0 ; i < playableTokens.Count ; i++)
        {
            if(tokensToSelect[i])
            {
                tokenToPlay = playableTokens[i];
            }
        }

        for(int i = 0 ; i < playableTokens.Count ; i++)
        {
            if(tokensToSelect[i])
            {
                if(tokenToPlay.CompareTo(playableTokens[i]) < 0)
                {
                    tokenToPlay = playableTokens[i];
                    result = i;
                }
            }
        }

        return result;
    }
}

class TableFrequencyStrategy : IStrategy
{
    public int ChooseTokenIndex(List<Tuple<Token, Position>> playableTokensAndPositions, Player player, List<Team> teams, Dictionary<Player, List<Token?>> playerBoard, List<Token> tableTokens, int center)
    {
        List<Token> playableTokens = new List<Token>();

        foreach(Tuple<Token, Position> playableToken in playableTokensAndPositions)
        {
            playableTokens.Add(playableToken.Item1);
        }
        
        if(playableTokens.Count == 0)return -1;

        Dictionary<string, int> idFrequency = new Dictionary<string, int>();

        foreach(Token token in tableTokens)
        {
            if(!idFrequency.ContainsKey(token.Faces.Item1.Id))
            {
                idFrequency.Add(token.Faces.Item1.Id, 0);
            }

            if(!idFrequency.ContainsKey(token.Faces.Item2.Id))
            {
                idFrequency.Add(token.Faces.Item2.Id, 0);
            }

            if(token.Faces.Item1.Id != token.Faces.Item2.Id)
            {
                idFrequency[token.Faces.Item1.Id]++;
                idFrequency[token.Faces.Item2.Id]++;
            }
            else
            {
                idFrequency[token.Faces.Item1.Id]++;
            }
        }

        string mostFrequencyId = "";
        int frequency = 0;

        foreach(var id in idFrequency)
        {
            if(id.Value > frequency)
            {
                mostFrequencyId = id.Key;
                frequency = id.Value;
            }
        }

        List<bool> tokensToSelect = new List<bool>();

        foreach(Token token in playableTokens)
        {
            tokensToSelect.Add(token.Faces.Item1.Id == mostFrequencyId || token.Faces.Item2.Id == mostFrequencyId);
        }

        Token tokenToPlay = new Token();
        int result = 0;

        for(int i = 0 ; i < playableTokens.Count ; i++)
        {
            if(tokensToSelect[i])
            {
                tokenToPlay = playableTokens[i];
            }
        }

        for(int i = 0 ; i < playableTokens.Count ; i++)
        {
            if(tokensToSelect[i])
            {
                if(tokenToPlay.CompareTo(playableTokens[i]) < 0)
                {
                    tokenToPlay = playableTokens[i];
                    result = i;
                }
            }
        }

        return result;
    }
}