using OsuFormatReader.Sections.EventTypes.EventParamsTypes;
using OsuFormatReader.Interfaces;

namespace OsuFormatReader.Sections.EventTypes;

public class VideosEvent(int startTime, VideosEventParams eventParams) : IEvent<VideosEventParams>
{
    public int eventType { get; } = 1;

    public int startTime { get; set; } = startTime;
    public VideosEventParams eventParams { get; set; } = eventParams;
}