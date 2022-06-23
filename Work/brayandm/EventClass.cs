/*
class <EventName> : Event
{
    public override void Start()
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
    public abstract void Start();
}

abstract class ComplexEvent : Event
{
    protected Event? Origin = null;
    
    protected HashSet<Event> Ends = new HashSet<Event>();

    private Dictionary<Event,List<Tuple<Event,Func<bool>>>> AdyList = new Dictionary<Event, List<Tuple<Event, Func<bool>>>>();

    protected void AddEdge(Event eventA, Event eventB, Func<bool> function)
    {
        if(!this.AdyList.ContainsKey(eventA))
        {
            this.AdyList.Add(eventA, new List<Tuple<Event, Func<bool>>>());
        }
        
        this.AdyList[eventA].Add(new Tuple<Event,Func<bool>>(eventB, function));
    }

    private bool StartAt(Event eventFather)
    {
        eventFather.Start();

        if(this.Ends.Contains(eventFather))
        {
            return true;
        }

        if(this.AdyList.ContainsKey(eventFather))
        {
            foreach(Tuple<Event,Func<bool>> eventChild in this.AdyList[eventFather])
            {
                if(eventChild.Item2())
                {
                    if(this.StartAt(eventChild.Item1))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public override void Start()
    {
        if(this.Origin is Event)
        {
            this.StartAt(this.Origin);
        }
    }
}