/*
public class <EventName> : Event
{
    public override void Action(Game game)
    {
        ... Code
    }
}

public class <ComplexEventName> : ComplexEvent
{
    public <ComplexEventName>()
    {
        Event event0 = new <EventName0>();
        Event event1 = new <EventName1>();
        Event event2 = new <EventName2>();
        Event event3 = new <EventName3>();
        ... More Events

        this.Origin = event0;

        this.Ends.Add(event1);
        this.Ends.Add(event2);
        this.Ends.Add(event3);
        ... More Ends

        AddEdge(event0, event1, function1);
        AddEdge(event0, event2, function2);
        AddEdge(event0, event3, function3);
        ... More Edges
    }
}
*/

// Esta clase representa un evento simple
public abstract class Event
{
    // Esta funcion es la accion a ejecutar
    public abstract void Action(Game game);

    // Esta funcion se usa para ser sobrecargada con los eventos complejos
    public virtual void Start(Game game, IGraphicInterface graphicinterface)
    {
        Action(game);

        graphicinterface.UpdateGame(game);
    }
}

// Esta clase representa un evento complejo
public abstract class ComplexEvent : Event
{
    // Aqui se almacena el evento inicial a ejecutar
    protected Event? Origin = null;
    
    // Aqui se almacenan los eventos de finalizacion
    protected HashSet<Event> Ends = new HashSet<Event>();

    // Aqui se almacena la lista de adyacencia
    private Dictionary<Event, List<Tuple<Event, Func<Game, bool>>>> AdyList = new Dictionary<Event, List<Tuple<Event, Func<Game, bool>>>>();

    // Esta funcion agrega una arista al grafo de eventos y estados
    protected void AddEdge(Event eventA, Event eventB, Func<Game, bool> function)
    {
        if(!this.AdyList.ContainsKey(eventA))
        {
            this.AdyList.Add(eventA, new List<Tuple<Event, Func<Game, bool>>>());
        }
        
        this.AdyList[eventA].Add(new Tuple<Event, Func<Game, bool>>(eventB, function));
    }

    // Esta funcion ejecuta recursivamente en pre-orden los nodos del grafo
    private bool StartAt(Event eventFather, Game game, IGraphicInterface graphicinterface)
    {
        eventFather.Start(game, graphicinterface);

        if(this.Ends.Contains(eventFather))
        {
            return true;
        }

        if(this.AdyList.ContainsKey(eventFather))
        {
            foreach(Tuple<Event, Func<Game, bool>> eventChild in this.AdyList[eventFather])
            {
                if(eventChild.Item2(game))
                {
                    if(this.StartAt(eventChild.Item1, game, graphicinterface))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    // Accion del juego complejo
    public override void Action(Game game){}

    // Esta funcion ejecuta el nodo inicial
    public override void Start(Game game, IGraphicInterface graphicinterface)
    {
        if(this.Origin is Event)
        {
            this.StartAt(this.Origin, game, graphicinterface);
        }
    }
}