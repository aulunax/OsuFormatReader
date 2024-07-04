namespace OsuFormatReader.Interfaces;

public interface IEvent
{
    int eventType { get; }
    int startTime { get; set; }
    object eventParams { get; set; }
}