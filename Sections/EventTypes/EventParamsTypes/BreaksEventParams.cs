namespace OsuFormatReader.Sections.EventTypes.EventParamsTypes;

public class BreaksEventParams
{
    public int endTime { get; set; }

    public BreaksEventParams(int endTime)
    {
        this.endTime = endTime;
    }
}