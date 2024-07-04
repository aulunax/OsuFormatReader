using OsuFormatReader.Sections.EventTypes.EventParamsTypes;
using OsuFormatReader.Interfaces;

namespace OsuFormatReader.Sections.EventTypes;

public class BackgroundsEvent(int startTime, BackgroundsEventParams eventParams) : IEvent<BackgroundsEventParams>
{
    public int eventType { get; } = 0;

    public int startTime { get; set; } = startTime;
    public BackgroundsEventParams eventParams { get; set; } = eventParams;
}