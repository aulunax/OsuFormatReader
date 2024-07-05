using OsuFormatReader.IO;

namespace OsuFormatReader.Sections;
public class Editor
{
    public List<int> Bookmarks { get; set; }
    public decimal DistanceSpacing { get; set; }
    public int BeatDivisor { get; set; }
    public int GridSize { get; set; }
    public decimal TimelineZoom { get; set; }
    
    public static Editor Read(OsuFormatReader reader, Editor? outobj = null)
    {
        if (outobj is null)
            outobj = new Editor();

        while (!reader.IsAtEnd && reader.SectionType == SectionType.Editor)
        {
            KeyValueReader.ReadAndUpdate(reader, outobj);
        }

        return outobj;
    }
}