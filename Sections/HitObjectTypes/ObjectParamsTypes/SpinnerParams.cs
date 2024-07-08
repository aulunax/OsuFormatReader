namespace OsuFormatReader.Sections.HitObjectTypes.ObjectParamsTypes;

public class SpinnerParams
{
    public SpinnerParams(int endTime)
    {
        this.endTime = endTime;
    }

    public int endTime { get; set; }
}