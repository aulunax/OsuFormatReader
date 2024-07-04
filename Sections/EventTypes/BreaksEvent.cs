using OsuFormatReader.Sections.EventTypes.EventParamsTypes;
using OsuFormatReader.Interfaces;

namespace OsuFormatReader.Sections.EventTypes;

public class BreaksEvent(int startTime, BreaksEventParams eventParams) : IEvent<BreaksEventParams>
{
    public int eventType { get; } = 2;

    public int startTime { get; set; } = startTime;
    public BreaksEventParams eventParams { get; set; } = eventParams;
}