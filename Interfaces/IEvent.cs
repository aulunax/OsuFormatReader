namespace OsuFormatReader.Interfaces;

public interface IEvent<TEventParams> : IEvent
{ 
    int eventType { get; }
    int startTime { get; set; }
    TEventParams eventParams { get; set; }
    object IEvent.eventParams
    {
        get => eventParams;
        set => eventParams = (TEventParams)value;
    }
}