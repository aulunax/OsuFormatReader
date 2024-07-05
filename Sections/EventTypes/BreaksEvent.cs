using OsuFormatReader.Enums;
using OsuFormatReader.Sections.EventTypes.EventParamsTypes;
using OsuFormatReader.Interfaces;

namespace OsuFormatReader.Sections.EventTypes;

public class BreaksEvent(int startTime, BreaksEventParams eventParams) : IEvent<BreaksEventParams>
{
    public EventType eventType { get; } = EventType.Break;

    public int startTime { get; set; } = startTime;
    public BreaksEventParams eventParams { get; set; } = eventParams;
}