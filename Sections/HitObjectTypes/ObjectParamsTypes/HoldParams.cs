namespace OsuFormatReader.Sections.HitObjectTypes.ObjectParamsTypes;

public class HoldParams
{
    public int endTime { get; set; }

    public HoldParams(int endTime)
    {
        this.endTime = endTime;
    }
}