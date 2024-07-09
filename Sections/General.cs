using OsuFormatReader.Enums;
using OsuFormatReader.IO;
using OsuFormatReader.Parsers;

namespace OsuFormatReader.Sections;

public class General
{
    public string AudioFilename { get; set; }
    public int AudioLeadIn { get; set; } = 0;
    public string AudioHash { get; set; } // Deprecated
    public int PreviewTime { get; set; } = -1;
    public int Countdown { get; set; } = 1;
    public string SampleSet { get; set; } = "Normal"; // TODO: Change to enum
    public decimal StackLeniency { get; set; } = new(0.7);
    public int Mode { get; set; } = 0; // TODO: Change to enum
    public bool LetterboxInBreaks { get; set; } = false;
    public bool StoryFireInFront { get; set; } = true; // Deprecated
    public bool UseSkinSprites { get; set; } = false;
    public bool AlwaysShowPlayfield { get; set; } = false; // Deprecated
    public string OverlayPosition { get; set; } = "NoChange"; // TODO: Change to enum
    public string SkinPreference { get; set; }
    public bool EpilepsyWarning { get; set; } = false;
    public int CountdownOffset { get; set; } = 0;
    public bool SpecialStyle { get; set; } = false;
    public bool WidescreenStoryboard { get; set; } = false;
    public bool SamplesMatchPlaybackRate { get; set; } = false;

    /// <summary>
    /// Used for transfer of <see cref="Editor"/> section key value pairs to actual Editor object.
    /// Necessary because of older .osu format versions.
    /// </summary>
    internal Editor? InternalEditor { get; private set; } = null;

    public static General Read(OsuFormatStreamReader reader, General? outobj = null)
    {
        if (outobj is null)
            outobj = new General();

        reader.ReadUntilSection(SectionType.General);

        while (!reader.IsAtEnd && reader.SectionType == SectionType.General)
        {
            string? value;
            var key = reader.TryReadKeyValuePair(out value);

            if (key == null || value == null)
                continue;

            if (key == "EditorDistanceSpacing" || key == "EditorBookmarks")
            {
                if (outobj.InternalEditor is null)
                    outobj.InternalEditor = new Editor();

                try
                {
                    KeyValueParser.UpdateProperty(key.Replace("Editor", ""), value, outobj.InternalEditor);
                }
                catch (FormatException e)
                {
                    reader.ReportParserError(e.Message);
                }
            }
            else
            {
                try
                {
                    KeyValueParser.UpdateProperty(key, value, outobj);
                }
                catch (FormatException e)
                {
                    reader.ReportParserError(e.Message);
                }
            }
        }

        return outobj;
    }
}