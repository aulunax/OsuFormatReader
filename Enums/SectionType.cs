using OsuFormatReader.Sections;

namespace OsuFormatReader;

public enum SectionType : byte
{
    None,
    General,
    Editor,
    Metadata,
    Difficulty,
    Events,
    TimingPoints,
    Colours,
    HitObjects
}

public static class SectionTypeExtensions
{
    public static SectionType ToSectionType(string sectionType)
    {
        switch (sectionType)
        {
            case "General": return SectionType.General;
            case "Editor": return SectionType.Editor;
            case "Metadata": return SectionType.Metadata;
            case "Difficulty": return SectionType.Difficulty;
            case "Events": return SectionType.Events;
            case "TimingPoints": return SectionType.TimingPoints;
            case "Colours": return SectionType.Colours;
            case "HitObjects": return SectionType.HitObjects;
            default: return SectionType.None;
        }
    }
}