namespace OsuFormatReader.Sections;

public class General
{
    public string AudioFilename { get; set; }
    public int AudioLeadIn { get; set; }
    public string AudioHash { get; set; } // Deprecated
    public int PreviewTime { get; set; }
    public int Countdown { get; set; }
    public string SampleSet { get; set; }
    public decimal StackLeniency { get; set; }
    public int Mode { get; set; }
    public bool LetterboxInBreaks { get; set; }
    public bool StoryFireInFront { get; set; } // Deprecated
    public bool UseSkinSprites { get; set; }
    public bool AlwaysShowPlayfield { get; set; } // Deprecated
    public string OverlayPosition { get; set; }
    public string SkinPreference { get; set; }
    public bool EpilepsyWarning { get; set; }
    public int CountdownOffset { get; set; }
    public bool SpecialStyle { get; set; }
    public bool WidescreenStoryboard { get; set; }
    public bool SamplesMatchPlaybackRate { get; set; }
}