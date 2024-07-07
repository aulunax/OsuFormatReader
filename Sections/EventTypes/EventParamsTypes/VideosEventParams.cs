namespace OsuFormatReader.Sections.EventTypes.EventParamsTypes;

public class VideosEventParams
{
    public string filename { get; set; }
    public int xOffset  { get; set; }
    public int yOffset  { get; set; }

    public VideosEventParams(string filename, int xOffset=0, int yOffset=0)
    {
        this.filename = filename;
        this.xOffset = xOffset;
        this.yOffset = yOffset;
    }
}