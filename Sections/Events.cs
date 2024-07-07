using OsuFormatReader.Enums;
using OsuFormatReader.Interfaces;
using OsuFormatReader.Parsers;

namespace OsuFormatReader.Sections;
public class Events
{
    private readonly List<IEvent> _events = new List<IEvent>();
    
    public void AddEvent(IEvent newEvent) 
    {
        _events.Add(newEvent);
    }
    
    public List<IEvent> GetEventsList() 
    {
        return _events;
    }

    public bool EventTypeExists(IEvent otherEvent)
    {
        return _events.Any(e => e.eventType == otherEvent.eventType);
    }
    
    public static Events Read(OsuFormatReader reader, Events? outobj = null)
    {
        if (outobj is null)
            outobj = new Events();

        while (!reader.IsAtEnd && reader.SectionType == SectionType.Events)
        {
            string? line = reader.ReadLine();

            if (line is null)
                continue;
            
            IEvent? newEvent = EventParser.ParseEvent(line);
            
            if (newEvent is null)
                continue;
            
            outobj.AddEvent(newEvent);
        }

        return outobj;
    }
}