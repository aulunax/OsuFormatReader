namespace OsuFormatReader.Sections.EventTypes.EventParamsTypes;

public class BreaksEventParams
{
    public BreaksEventParams(int endTime)
    {
        this.endTime = endTime;
    }

    public int endTime { get; set; }
}