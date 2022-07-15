class ProtectedToken : IComparable
{
    private Token _token;

    private List<Player> _ownersHistory = new List<Player>();

    private HashSet<Player> _visibles = new HashSet<Player>();

    public ProtectedToken(Token token)
    {
        this._token = token;
    }

    // Esta funcion agrega un nuevo propietario
    //a este objeto.
    public void NewOwner(Player player)
    {
        this._ownersHistory.Add(player);
    }

    // Esta funcion devuelve el propietario 
    //actual de este objeto.
    public Player? GetCurrentOwner()
    {
        return this._ownersHistory.Count > 0 ? this._ownersHistory.Last() : null; 
    }

    // Esta funcion rota el token
    public void Rotate()
    {
        this._token.Rotate();
    }

    // Esta funcion le brinda visibilidad a
    //un nuevo jugador.
    public void Watch(Player player)
    {
        if(!IsVisible(player)) 
        {
            this._visibles.Add(player);
        }
    }
    
    // Esta funcion le retira la visibilidad a
    //un jugador.
    public void Forget(Player player)
    {
        if(IsVisible(player)) 
        {
            this._visibles.Remove(player);
        }
    }
    
    // Esta propiedad comprueba si el jugador "player"
    //puede acceder al Token de este onjeto y devuelve true
    //en caso afirmativo, y false en caso contrario.
    public bool IsVisible(Player player)
    {
        return this._visibles.Contains(player);
    }

    // Esta funcion devuelve el Token almacenado en este objeto,
    //si el mismo es visible para el jugador "player",
    //en caso contrario devuelve null.
    public Token? GetToken(Player player)
    {
        return IsVisible(player) ? this._token : null;
    }

    // Esta funcion devuelve el Token almacenado en este objeto.
    public Token GetTokenWithoutVisibility()
    {
        return this._token;
    }

    // Esta funcion compara este objeto con obj
    //mediante la propiedad _token.
    public int CompareTo(object? obj)
    {
        if(obj == null || obj.GetType() != this.GetType())
        {
            return -1;
        }

        return this._token.CompareTo(((ProtectedToken)obj)._token);
    }
}



