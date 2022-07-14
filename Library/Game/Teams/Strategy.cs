interface IStrategy : IBaseInterface
{
    int ChooseTokenIndex(List<Token> playableTokens, Player player, List<Team> teams, Dictionary<Player, List<Token?>> playerBoard, List<Token> tableTokens);
}

class GreedyStrategy : IStrategy
{
    public int ChooseTokenIndex(List<Token> playableTokens, Player player, List<Team> teams, Dictionary<Player, List<Token?>> playerBoard, List<Token> tableTokens)
    {
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
    public int ChooseTokenIndex(List<Token> playableTokens, Player player, List<Team> teams, Dictionary<Player, List<Token?>> playerBoard, List<Token> tableTokens)
    {
        if(playableTokens.Count == 0)return -1;

        return new Random().Next(playableTokens.Count);
    }
}

class FrequencyStrategy : IStrategy
{
    public int ChooseTokenIndex(List<Token> playableTokens, Player player, List<Team> teams, Dictionary<Player, List<Token?>> playerBoard, List<Token> tableTokens)
    {
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