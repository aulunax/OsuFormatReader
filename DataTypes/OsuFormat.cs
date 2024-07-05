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

    public static OsuFormat Read(OsuFormatReader reader, OsuFormat? outobj = null)
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
            case SectionType.General: outobj.General = General.Read(reader);
                break;
            case SectionType.Editor: outobj.Editor = Editor.Read(reader);
                break;
            case SectionType.Metadata: outobj.Metadata = Metadata.Read(reader);
                break;
            case SectionType.Difficulty: outobj.Difficulty = Difficulty.Read(reader);
                break;
            case SectionType.Events: outobj.Events = Events.Read(reader);
                break;
            case SectionType.TimingPoints: outobj.TimingPoints = TimingPoints.Read(reader);
                break;
            case SectionType.Colours: outobj.Colours = Colours.Read(reader);
                break;
            case SectionType.HitObjects: outobj.HitObjects = HitObjects.Read(reader);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(sectionType), sectionType, null);
        }
    }
}