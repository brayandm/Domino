class ProtectedToken : IComparable
{
    private Token _token;

    private List<Player> _ownersHistory = new List<Player>();

    private HashSet<Player> _visibles = new HashSet<Player>();

    public ProtectedToken(Token token)
    {
        this._token = token;
    }

    public void NewOwner(Player player)
    {
        this._ownersHistory.Add(player);
    }

    public Player? GetCurrentOwner()
    {
        return this._ownersHistory.Count > 0 ? this._ownersHistory.Last() : null; 
    }

    public void Rotate()
    {
        this._token.Rotate();
    }

    public void Watch(Player player)
    {
        if(!IsVisible(player)) 
        {
            this._visibles.Add(player);
        }
    }
    
    public void Forget(Player player)
    {
        if(IsVisible(player)) 
        {
            this._visibles.Remove(player);
        }
    }
    
    public bool IsVisible(Player player)
    {
        return this._visibles.Contains(player);
    }

    public Token? GetToken(Player player)
    {
        return IsVisible(player) ? this._token : null;
    }

    public Token GetTokenWithoutVisibility()
    {
        return this._token;
    }

    public int CompareTo(object? obj)
    {
        if(obj == null || obj.GetType() != this.GetType())
        {
            return -1;
        }

        return this._token.CompareTo(((ProtectedToken)obj)._token);
    }
}