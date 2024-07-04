using OsuFormatReader.Sections;

namespace OsuFormatReader.DataTypes;

public class OsuFormat
{
    public General General;
    public Editor Editor;
    public Metadata Metadata;
    public Difficulty Difficulty;
    public Events Events;
    public TimingPoints TimingPoints;
    public Colours Colours;
    public HitObjects HitObjects;

    public OsuFormat(General general, Editor editor, Metadata metadata, Difficulty difficulty, Events events, TimingPoints timingPoints, Colours colours, HitObjects hitObjects)
    {
        this.General = general;
        this.Editor = editor;
        this.Metadata = metadata;
        this.Difficulty = difficulty;
        this.Events = events;
        this.TimingPoints = timingPoints;
        this.Colours = colours;
        this.HitObjects = hitObjects;
    }
    
    private OsuFormat() { }

    public static OsuFormat Read(OsuFormatReader reader, OsuFormat outobj = null)
    {
        if (outobj is null)
            outobj = new OsuFormat();

        reader.ReadUntilFirstSection();
        while (!reader.IsAtEnd)
        {
            ReadSection(reader, reader.SectionType, outobj);
        }
        

        return outobj;
    }

    private static void ReadSection(OsuFormatReader reader, SectionType sectionType, OsuFormat outobj)
    {
        switch (sectionType)
        {
            case SectionType.General: General.Read(reader, outobj.General);
                break;
            case SectionType.Editor: Editor.Read(reader, outobj.Editor);
                break;
            case SectionType.Metadata: Metadata.Read(reader, outobj.Metadata);
                break;
            case SectionType.Difficulty: Difficulty.Read(reader, outobj.Difficulty);
                break;
            case SectionType.Events: Events.Read(reader, outobj.Events);
                break;
            case SectionType.TimingPoints: TimingPoints.Read(reader, outobj.TimingPoints);
                break;
            case SectionType.Colours: Colours.Read(reader, outobj.Colours);
                break;
            case SectionType.HitObjects: HitObjects.Read(reader, outobj.HitObjects);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(sectionType), sectionType, null);
        }
    }
}