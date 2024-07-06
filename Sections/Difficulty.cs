using OsuFormatReader.IO;

namespace OsuFormatReader.Sections;
public class Difficulty
{
    public decimal HPDrainRate { get; set; }
    public decimal CircleSize { get; set; }
    public decimal OverallDifficulty { get; set; }
    public decimal ApproachRate { get; set; }
    public decimal SliderMultiplier { get; set; }
    public decimal SliderTickRate { get; set; }
    
    public static Difficulty Read(OsuFormatReader reader, Difficulty? outobj = null)
    {
        if (outobj is null)
            outobj = new Difficulty();

        while (!reader.IsAtEnd && reader.SectionType == SectionType.Difficulty)
        {
            KeyValueReader.ReadAndUpdateProperty(reader, outobj);
        }

        return outobj;
    }
}