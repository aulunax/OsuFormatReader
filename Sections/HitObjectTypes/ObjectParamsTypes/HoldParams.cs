namespace OsuFormatReader.Sections.HitObjectTypes.ObjectParamsTypes;

public class HoldParams
{
    public HoldParams(int endTime)
    {
        this.endTime = endTime;
    }

    public int endTime { get; set; }
}