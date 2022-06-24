interface IStrategy : IBaseInterface
{
    int ChooseTokenIndex(List<Token> tokens);
}

class GreedyStrategy : IStrategy
{
    public int ChooseTokenIndex(List<Token> tokens)
    {
        if(tokens.Count == 0)return -1;
     
        Token token = tokens[0];
        int result = 0;

        for(int i = 0 ; i < tokens.Count ; i++)
        {
            if(token.CompareTo(tokens[i]) < 0)
            {
                token = tokens[i];
                result = i;
            }
        }

        return result;
    }
}

class RandomStrategy : IStrategy
{
    public int ChooseTokenIndex(List<Token> tokens)
    {
        if(tokens.Count == 0)return -1;

        return new Random().Next(tokens.Count);
    }
}