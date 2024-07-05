using OsuFormatReader.Interfaces;

namespace OsuFormatReader.Sections;
public class Events
{
    private readonly List<IEvent> _events = new List<IEvent>();
    
    public void AddEvent(IEvent newEvent) 
    {
        if (eventTypeExists(newEvent))
        {
            // Maybe replace the event here?
            return;
        }
            
        _events.Add(newEvent);
    }
    
    public IEvent? GetEvent(int eventType) 
    {
        return _events.Find(e => e.eventType == eventType);
    }
    
    public List<IEvent> GetEventsList() 
    {
        return _events;
    }

    private bool eventTypeExists(IEvent otherEvent)
    {
        return _events.Any(e => e.eventType == otherEvent.eventType);
    }
    
    public static Events Read(OsuFormatReader reader, Events? outobj = null)
    {
        reader.ReadLine();
        return null;
    }
}