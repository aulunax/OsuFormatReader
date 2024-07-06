using OsuFormatReader.IO;

namespace OsuFormatReader.Sections;
public class Metadata
{
    public string Title { get; set; }
    public string TitleUnicode { get; set; }
    public string Artist { get; set; }
    public string ArtistUnicode { get; set; }
    public string Creator { get; set; }
    public string Version { get; set; }
    public string Source { get; set; }
    public string Tags { get; set; }
    public int BeatmapID { get; set; }
    public int BeatmapSetID { get; set; }

    public List<string>? GetTagsAsList()
    {
        if (string.IsNullOrEmpty(Tags))
            return null;
        
        return Tags.Split(' ').ToList();;
    }
    
    public static Metadata Read(OsuFormatReader reader, Metadata? outobj = null)
    {
        if (outobj is null)
            outobj = new Metadata();

        while (!reader.IsAtEnd && reader.SectionType == SectionType.Metadata)
        {
            KeyValueReader.ReadAndUpdateProperty(reader, outobj);
        }

        return outobj;
    }
}