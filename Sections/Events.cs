using OsuFormatReader.Enums;
using OsuFormatReader.Interfaces;
using OsuFormatReader.IO;
using OsuFormatReader.Parsers;

namespace OsuFormatReader.Sections;

public class Events
{
    private readonly List<IEvent> _events = new();

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

    public static Events Read(OsuFormatStreamReader reader, Events? outobj = null)
    {
        if (outobj is null)
            outobj = new Events();

        reader.ReadUntilSection(SectionType.Events);

        while (!reader.IsAtEnd && reader.SectionType == SectionType.Events)
        {
            var line = reader.ReadParsedLine();

            if (line is null)
                continue;

            try
            {
                var newEvent = EventParser.ParseEvent(line);

                if (newEvent is null)
                {
                    // Unrecognized event (most likely storyboard)
                    // reader.ReportParserError($"Invalid event");
                    continue;
                }

                outobj.AddEvent(newEvent);
            }
            catch (FormatException e)
            {
                reader.ReportParserError(e.Message);
            }
        }

        return outobj;
    }
}