class ProtectedToken : IComparable
{
    private Token Token;

    private HashSet<Player> Visibles;

    public ProtectedToken(Token token)
    {
        this.Token = token;
        this.Visibles = new HashSet<Player>();
    }

    public void Watch(Player player)
    {
        if(!IsVisible(player)) 
        {
            this.Visibles.Add(player);
        }
    }
    
    public void Forget(Player player)
    {
        if(IsVisible(player)) 
        {
            this.Visibles.Remove(player);
        }
    }
    
    public bool IsVisible(Player player)
    {
        return this.Visibles.Contains(player);
    }

    public Token? GetToken(Player player)
    {
        return IsVisible(player) ? this.Token : null;
    }

    public Token GetTokenWithoutVisibility()
    {
        return this.Token;
    }

    public int CompareTo(object? obj)
    {
        if(obj == null || obj.GetType() != this.GetType())
        {
            return -1;
        }

        return this.Token.CompareTo(((ProtectedToken)obj).Token);
    }
}



