using OsuFormatReader.Enums;
using OsuFormatReader.IO;
using OsuFormatReader.Parsers;

namespace OsuFormatReader.Sections;

public class Difficulty
{
    public decimal HPDrainRate { get; set; }
    public decimal CircleSize { get; set; }
    public decimal OverallDifficulty { get; set; }
    public decimal ApproachRate { get; set; }
    public decimal SliderMultiplier { get; set; }
    public decimal SliderTickRate { get; set; }

    public static Difficulty Read(OsuFormatStreamReader reader, Difficulty? outobj = null)
    {
        if (outobj is null)
            outobj = new Difficulty();

        reader.ReadUntilSection(SectionType.Difficulty);
        
        while (!reader.IsAtEnd && reader.SectionType == SectionType.Difficulty)
            KeyValueParser.ReadAndUpdateProperty(reader, outobj);

        if (reader.FormatVersion < 9)
            outobj.ApproachRate = outobj.OverallDifficulty;

        return outobj;
    }
}