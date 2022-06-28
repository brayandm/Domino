/*
class <EventName> : Event
{
    public override void Action(Game game)
    {
        ... Code
    }
}

class <ComplexEventName> : ComplexEvent
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

abstract class Event
{
    public abstract void Action(Game game);

    public void Start(Game game, IGraphicinterface graphicinterface)
    {
        Action(game);

        graphicinterface.Update(game);
    }
}

abstract class ComplexEvent : Event
{
    protected Event? Origin = null;
    
    protected HashSet<Event> Ends = new HashSet<Event>();

    private Dictionary<Event, List<Tuple<Event, Func<Game, bool>>>> AdyList = new Dictionary<Event, List<Tuple<Event, Func<Game, bool>>>>();

    protected void AddEdge(Event eventA, Event eventB, Func<Game, bool> function)
    {
        if(!this.AdyList.ContainsKey(eventA))
        {
            this.AdyList.Add(eventA, new List<Tuple<Event, Func<Game, bool>>>());
        }
        
        this.AdyList[eventA].Add(new Tuple<Event, Func<Game, bool>>(eventB, function));
    }

    private bool StartAt(Event eventFather, Game game, IGraphicinterface graphicinterface)
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

    public override void Action(Game game){}

    public new void Start(Game game, IGraphicinterface graphicinterface)
    {
        if(this.Origin is Event)
        {
            this.StartAt(this.Origin, game, graphicinterface);
        }
    }
}