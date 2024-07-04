namespace OsuFormatReader.Sections;
public class Editor
{
    public List<int> Bookmarks { get; set; }
    public decimal DistanceSpacing { get; set; }
    public int BeatDivisor { get; set; }
    public int GridSize { get; set; }
    public decimal TimelineZoom { get; set; }
    
    public static void Read(OsuFormatReader reader, Editor outobj)
    {
       
    }
}