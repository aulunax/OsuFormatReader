namespace OsuFormatReader.Sections.EventTypes.EventParamsTypes;

public class BackgroundsEventParams
{
    public string filename { get; set; }
    public int xOffset  { get; set; }
    public int yOffset  { get; set; }
    
    public BackgroundsEventParams(string filename, int xOffset, int yOffset)
    {
        this.filename = filename;
        this.xOffset = xOffset;
        this.yOffset = yOffset;
    }
}