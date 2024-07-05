using OsuFormatReader.Enums;
using OsuFormatReader.Sections.EventTypes.EventParamsTypes;
using OsuFormatReader.Interfaces;

namespace OsuFormatReader.Sections.EventTypes;

public class BackgroundsEvent : IEvent<BackgroundsEventParams>
{
    public BackgroundsEvent(BackgroundsEventParams eventParams)
    {
        this.eventParams = eventParams;
    }

    public EventType eventType { get; } = EventType.Background;

    public int startTime { get; set; } = 0;
    public BackgroundsEventParams eventParams { get; set; }
}