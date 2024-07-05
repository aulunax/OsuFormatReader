using OsuFormatReader.DataTypes;

namespace OsuFormatReader.Sections;

public class TimingPoints
{
    private List<TimingPoint> _timingPoints = new List<TimingPoint>();
    
    public static TimingPoints Read(OsuFormatReader reader, TimingPoints? outobj = null)
    {
        reader.ReadLine();
        return null;

    }
}