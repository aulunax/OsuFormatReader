using OsuFormatReader.Enums;
using OsuFormatReader.IO;
using OsuFormatReader.Sections;

namespace OsuFormatReader.DataTypes;

/// <summary>
/// Represents the .osu format file .
/// </summary>
public class OsuFormat
{
    /// <summary>
    /// Gets or sets the version of the .osu format.
    /// </summary>
    public int FormatVersion { get; set; }

    /// <summary>
    /// Gets or sets the Colours section of the .osu format file.
    /// </summary>
    public Colours Colours;

    /// <summary>
    /// Gets or sets the Difficulty section of the .osu format file.
    /// </summary>
    public Difficulty Difficulty;

    /// <summary>
    /// Gets or sets the Editor section of the .osu format file.
    /// </summary>
    public Editor Editor;

    /// <summary>
    /// Gets or sets the Events section of the .osu format file.
    /// </summary>
    public Events Events;

    /// <summary>
    /// Gets or sets the General section of the .osu format file.
    /// </summary>
    public General General;

    /// <summary>
    /// Gets or sets the HitObjects section of the .osu format file.
    /// </summary>
    public HitObjects HitObjects;

    /// <summary>
    /// Gets or sets the Metadata section of the .osu format file.
    /// </summary>
    public Metadata Metadata;

    /// <summary>
    /// Gets or sets the TimingPoints section of the .osu format file.
    /// </summary>
    public TimingPoints TimingPoints;


    /// <summary>
    /// Initializes a new instance of the <see cref="OsuFormat"/> class with the specified sections.
    /// </summary>
    /// <param name="general">The <see cref="Sections.General"/> section of the .osu format file.</param>
    /// <param name="editor">The <see cref="Sections.Editor"/> section of the .osu format file.</param>
    /// <param name="metadata">The <see cref="Sections.Metadata"/> section of the .osu format file.</param>
    /// <param name="difficulty">The <see cref="Sections.Difficulty"/> section of the .osu format file.</param>
    /// <param name="events">The <see cref="Sections.Events"/> section of the .osu format file.</param>
    /// <param name="timingPoints">The <see cref="Sections.TimingPoints"/> section of the .osu format file.</param>
    /// <param name="colours">The <see cref="Sections.Colours"/> section of the .osu format file.</param>
    /// <param name="hitObjects">The <see cref="Sections.HitObjects"/> section of the .osu format file.</param>
    public OsuFormat(General general, Editor editor, Metadata metadata, Difficulty difficulty, Events events,
        TimingPoints timingPoints, Colours colours, HitObjects hitObjects)
    {
        General = general;
        Editor = editor;
        Metadata = metadata;
        Difficulty = difficulty;
        Events = events;
        TimingPoints = timingPoints;
        Colours = colours;
        HitObjects = hitObjects;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OsuFormat"/> class with all section fields set to null.
    /// </summary>
    private OsuFormat()
    {
    }

    /// <summary>
    /// Reads the .osu format file from the specified <see cref="OsuFormatStreamReader"/>.
    /// </summary>
    /// <param name="reader">The reader to read the .osu  format file from.</param>
    /// <param name="outobj">An optional existing instance of <see cref="OsuFormat"/> to populate. If null, a new instance will be created.</param>
    /// <param name="mask">Defines which sections of the format should be read.</param>
    /// <returns>The populated <see cref="OsuFormat"/> object.</returns>
    public static OsuFormat Read(OsuFormatStreamReader reader, OsuFormat? outobj = null, SectionType mask = SectionType.All)
    {
        if (outobj is null)
            outobj = new OsuFormat();

        reader.ReadUntilNextSection();
        outobj.FormatVersion = reader.FormatVersion;
        while (!reader.IsAtEnd) ReadSection(reader, reader.SectionType, outobj, mask);


        return outobj;
    }

    /// <summary>
    /// Reads the specified section from the <see cref="OsuFormatStreamReader"/> and populates the corresponding section in the <see cref="OsuFormat"/> object.
    /// </summary>
    /// <param name="reader">The reader to read the section from.</param>
    /// <param name="sectionType">The <see cref="SectionType"/> type of the section being read.</param>
    /// <param name="outobj">The <see cref="OsuFormat"/> object to populate.</param>
    private static void ReadSection(OsuFormatStreamReader reader, SectionType sectionType, OsuFormat outobj, SectionType mask)
    {
        switch (sectionType)
        {
            case SectionType.General:
                if (!mask.HasFlag(SectionType.General))
                {
                    reader.ReadUntilNextSection();
                    break;
                }
                outobj.General = General.Read(reader);
                outobj.Editor = outobj.General.InternalEditor is not null ? outobj.General.InternalEditor : null;
                break;
            case SectionType.Editor:
                if (!mask.HasFlag(SectionType.Editor))
                {
                    reader.ReadUntilNextSection();
                    break;
                }
                outobj.Editor = Editor.Read(reader);
                break;
            case SectionType.Metadata:
                if (!mask.HasFlag(SectionType.Metadata))
                {
                    reader.ReadUntilNextSection();
                    break;
                }
                outobj.Metadata = Metadata.Read(reader);
                break;
            case SectionType.Difficulty:
                if (!mask.HasFlag(SectionType.Difficulty))
                {
                    reader.ReadUntilNextSection();
                    break;
                }
                outobj.Difficulty = Difficulty.Read(reader);
                break;
            case SectionType.Events:
                if (!mask.HasFlag(SectionType.Events))
                {
                    reader.ReadUntilNextSection();
                    break;
                }
                outobj.Events = Events.Read(reader);
                break;
            case SectionType.TimingPoints:
                if (!mask.HasFlag(SectionType.TimingPoints))
                {
                    reader.ReadUntilNextSection();
                    break;
                }
                outobj.TimingPoints = TimingPoints.Read(reader);
                break;
            case SectionType.Colours:
                if (!mask.HasFlag(SectionType.Colours))
                {
                    reader.ReadUntilNextSection();
                    break;
                }
                outobj.Colours = Colours.Read(reader);
                break;
            case SectionType.HitObjects:
                if (!mask.HasFlag(SectionType.HitObjects))
                {
                    reader.ReadUntilNextSection();
                    break;
                }
                outobj.HitObjects = HitObjects.Read(reader);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(sectionType), sectionType, null);
        }
    }
}