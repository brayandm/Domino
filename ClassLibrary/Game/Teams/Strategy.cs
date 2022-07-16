// Esta interfaz representa una estrategia
public interface IStrategy : IBaseInterface
{
    int ChooseTokenIndex(List<Tuple<Token, Position>> playableTokensAndPositions, Player player, List<Team> teams, Dictionary<Player, List<Token?>> playerBoard, List<Token> tableTokens, int center);
}

// Esta clase representa una estrategia de seleccion humana
public class HumanSelection : IStrategy
{
    public int ChooseTokenIndex(List<Tuple<Token, Position>> playableTokensAndPositions, Player player, List<Team> teams, Dictionary<Player, List<Token?>> playerBoard, List<Token> tableTokens, int center)
    {
        if(playableTokensAndPositions.Count == 0)
        {
            ((IGraphicInterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicInterface))).SendMessage("HumanSelection", "There is not token to play");
            
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

        ((IGraphicInterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicInterface))).Action("ConsoleClear");
        ((IGraphicInterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicInterface))).SendMessage("ShowPlayableTokens", player.Name + " Board:\n\n");
        ((IGraphicInterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicInterface))).SendMessage("ShowPlayableTokens", ((IGraphicInterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicInterface))).GraphicNullableBoard(playerBoard[player]));
        ((IGraphicInterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicInterface))).SendMessage("ShowPlayableTokens", "\n\n");
        ((IGraphicInterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicInterface))).SendMessage("ShowPlayableTokens", "Table:\n\n");
        ((IGraphicInterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicInterface))).SendMessage("ShowPlayableTokens", ((IGraphicInterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicInterface))).GraphicTable(tableTokens, center));
        ((IGraphicInterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicInterface))).SendMessage("ShowPlayableTokens", "\n\n");
        ((IGraphicInterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicInterface))).SendMessage("ShowPlayableTokens", "Playable Tokens:\n\n");
        ((IGraphicInterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicInterface))).SendMessage("ShowPlayableTokens", ((IGraphicInterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicInterface))).GraphicBoardAndPositions(playableTokensAndPositions));
        ((IGraphicInterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicInterface))).SendMessage("ShowPlayableTokens", "\n\n");

        string entry = ((IGraphicInterface)DependencyContainerRegister.Getter.GetInstance(typeof(IGraphicInterface))).GetEntry("HumanSelection", "Insert the number of the token to play (must be in range [1, " + playableTokensAndPositions.Count + "])", Func);
    
        return int.Parse(entry) - 1;
    }
}

// Esta clase representa una estrategia greedy
public class GreedyStrategy : IStrategy
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

// Esta clase representa una estrategia random
public class RandomStrategy : IStrategy
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

// Esta clase representa una estrategia por frecuencia de caras en el board
public class FrequencyStrategy : IStrategy
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

// Esta clase representa una estrategia greedy por frecuencias de caras en la mesa
public class TableFrequencyStrategy : IStrategy
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