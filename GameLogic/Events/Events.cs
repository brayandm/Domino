class Events
{
    private class PrintHelloWorld : Event
    {
        public override void Action(Game game)
        {
            System.Console.WriteLine("Hello World");
        }
    }

    public class MainEvent : ComplexEvent
    {
        public MainEvent()
        {
            Event firstEvent = new PrintHelloWorld();

            this.Origin = firstEvent;
        }
    }
}