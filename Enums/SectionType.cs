namespace OsuFormatReader.Enums;

[Flags]
public enum SectionType : byte
{
    None = 0,
    General = 1 << 0,
    Editor = 1 << 1,
    Metadata = 1 << 2,
    Difficulty = 1 << 3,
    Events = 1 << 4,
    TimingPoints = 1 << 5,
    Colours = 1 << 6,
    HitObjects = 1 << 7,
    All = General | Editor | Metadata | Difficulty | Events | TimingPoints | Colours | HitObjects
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