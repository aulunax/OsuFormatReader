namespace OsuFormatReader.Enums;

public enum SliderCurveType : byte
{
    Beizer = (byte)'B',
    Catmull = (byte)'C',
    Linear  = (byte)'L',
    PerfectCircle = (byte)'P'
}