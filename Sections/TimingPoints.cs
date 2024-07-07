using OsuFormatReader.DataTypes;
using OsuFormatReader.Parsers;

namespace OsuFormatReader.Sections;

public class TimingPoints
{
    private List<TimingPoint> _timingPoints = new List<TimingPoint>();

    public List<TimingPoint> GetTimingPoints()
    {
        return _timingPoints;
    }

    public void AddTimingPoint(TimingPoint timingPoint)
    {
        _timingPoints.Add(timingPoint);
    }
    
    public static TimingPoints Read(OsuFormatReader reader, TimingPoints? outobj = null)
    {

        if (outobj is null)
            outobj = new TimingPoints();

        while (!reader.IsAtEnd && reader.SectionType == SectionType.TimingPoints)
        {
            string? line = reader.ReadLine();

            if (line is null)
                continue;
            
            TimingPoint? newTimingPoint = ValueParser.ParseTimingPoint(line);
            
            if (newTimingPoint is null)
                continue;
            
            outobj.AddTimingPoint(newTimingPoint);
        }

        return outobj;
    }
}