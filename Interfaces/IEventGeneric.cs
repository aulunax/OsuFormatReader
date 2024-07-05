using OsuFormatReader.Enums;

namespace OsuFormatReader.Interfaces;

public interface IEvent
{
    EventType eventType { get; }
    int startTime { get; set; }
    object eventParams { get; set; }
}