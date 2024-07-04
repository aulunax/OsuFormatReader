namespace OsuFormatReader.Sections.EventTypes.EventParamsTypes;

public class VideosEventParams
{
    public string filename { get; set; }
    public int xOffset  { get; set; }
    public int yOffset  { get; set; }

    public VideosEventParams(string filename, int xOffset, int yOffset)
    {
        this.filename = filename;
        this.xOffset = xOffset;
        this.yOffset = yOffset;
    }
}