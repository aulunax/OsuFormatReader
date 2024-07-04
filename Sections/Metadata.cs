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
}