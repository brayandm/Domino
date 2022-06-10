/*
class <EventName> : Event
{
    public <EventName>()
    {
        this.Type = <Type>;
        this.Description = <Description>;
    }

    public override void Start()
    {
        <Code>
    }
}
*/

abstract class Event
{
    public string Type = "No Type";

    public string Description = "No Description";

    public abstract void Start();
}

abstract class ComplexEvent : Event
{
    public Event Origin;
    
    public HashSet<Event> Ends;

    public Dictionary<Event,List<Tuple<Event,Func<bool>>>> AdyList = new Dictionary<Event, List<Tuple<Event, Func<bool>>>>();

    public ComplexEvent(Event origin, List<Event> ends)
    {
        this.Origin = origin;
        this.Ends = ends.ToHashSet();
    }

    public void AddEdge(Event eventA, Event eventB, Func<bool> function)
    {
        if(!this.AdyList.ContainsKey(eventA))
        {
            this.AdyList.Add(eventA, new List<Tuple<Event, Func<bool>>>());
        }
        
        this.AdyList[eventA].Add(new Tuple<Event,Func<bool>>(eventB, function));
    }

    bool StartAt(Event eventFather)
    {
        eventFather.Start();

        if(Ends.Contains(eventFather))
        {
            return true;
        }

        foreach(Tuple<Event,Func<bool>> eventChild in AdyList[eventFather])
        {
            if(eventChild.Item2())
            {
                if(StartAt(eventChild.Item1))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public override void Start()
    {
        StartAt(this.Origin);
    }
}