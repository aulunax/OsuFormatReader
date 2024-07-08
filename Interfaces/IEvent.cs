namespace OsuFormatReader.Interfaces;

public interface IEvent<TEventParams> : IEvent
{
    TEventParams eventParams { get; set; }

    object IEvent.eventParams
    {
        get => eventParams;
        set => eventParams = (TEventParams)value;
    }
}