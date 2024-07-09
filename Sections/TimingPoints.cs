using OsuFormatReader.DataTypes;
using OsuFormatReader.Enums;
using OsuFormatReader.IO;
using OsuFormatReader.Parsers;

namespace OsuFormatReader.Sections;

public class TimingPoints
{
    private readonly List<TimingPoint> _timingPoints = new List<TimingPoint>();

    public List<TimingPoint> GetTimingPoints()
    {
        return _timingPoints;
    }

    public void AddTimingPoint(TimingPoint timingPoint)
    {
        _timingPoints.Add(timingPoint);
    }

    public static TimingPoints Read(OsuFormatStreamReader reader, TimingPoints? outobj = null)
    {
        if (outobj is null)
            outobj = new TimingPoints();

        reader.ReadUntilSection(SectionType.TimingPoints);

        while (!reader.IsAtEnd && reader.SectionType == SectionType.TimingPoints)
        {
            var line = reader.ReadParsedLine();

            if (line is null)
                continue;

            try
            {
                var newTimingPoint = ValueParser.ParseTimingPoint(line, reader.FormatVersion);

                if (newTimingPoint is null)
                    continue;

                outobj.AddTimingPoint(newTimingPoint);
            }
            catch (FormatException e)
            {
                reader.ReportParserError(e.Message);
            }
        }

        return outobj;
    }
}