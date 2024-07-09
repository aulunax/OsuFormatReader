using OsuFormatReader.Enums;
using OsuFormatReader.IO;
using OsuFormatReader.Parsers;

namespace OsuFormatReader.Sections;

public class Editor
{
    public List<int> Bookmarks { get; set; }
    public decimal DistanceSpacing { get; set; }
    public int BeatDivisor { get; set; }
    public int GridSize { get; set; }
    public decimal TimelineZoom { get; set; }

    public static Editor Read(OsuFormatStreamReader reader, Editor? outobj = null)
    {
        if (outobj is null)
            outobj = new Editor();

        reader.ReadUntilSection(SectionType.Editor);

        while (!reader.IsAtEnd && reader.SectionType == SectionType.Editor)
            KeyValueParser.ReadAndUpdateProperty(reader, outobj);

        return outobj;
    }
}