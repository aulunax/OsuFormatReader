namespace OsuFormatReader.Enums;

public enum EventType
{
    Background,
    Video,
    Break,
    Unknown
}

public static class EventTypeExtensions
{
    public static EventType StringToEventType(string eventType)
    {
        switch (eventType)
        {
            case "Background": return EventType.Background;
            case "Video": return EventType.Video;
            case "Break": return EventType.Break;
            default: return EventType.Unknown;
        }
    }
    
    public static EventType IntToEventType(int eventType)
    {
        switch (eventType)
        {
            case 0: return EventType.Background;
            case 1: return EventType.Video;
            case 2: return EventType.Break;
            default: return EventType.Unknown;
        }
    }
}