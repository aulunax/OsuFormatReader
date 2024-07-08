using OsuFormatReader.Enums;

namespace OsuFormatReader.DataTypes;

public class TimingPoint
{
    public int time { get; set; }

    /// <summary>
    ///     Wiki description:
    ///     https://osu.ppy.sh/wiki/en/Client/File_formats/osu_%28file_format%29#hitsounds:~:text=beatLength%20(Decimal)%3A,as%20SliderMultiplier.
    /// </summary>
    /// <remarks>
    /// Note: Chosen to use double instead of decimal as the type, because maps like this exist:
    ///     https://osu.ppy.sh/beatmapsets/1219078#osu/2536330,
    ///     in which timing points can have beatLength be NaN, which cannot be represented as decimal.
    /// </remarks>
    public double beatLength { get; set; }

    public int meter { get; set; }
    public int sampleSet { get; set; }
    public int sampleIndex { get; set; }
    public int volume { get; set; }
    public bool uninherited { get; set; }
    public Effects effects { get; set; }
    
    public TimingPoint()
    {
    }

    public TimingPoint(int time, double beatLength, int meter = 4, int sampleSet = 0, int sampleIndex = 0,
        int volume = 0, bool uninherited = true, Effects effects = 0)
    {
        this.time = time;
        this.beatLength = beatLength;
        this.meter = meter;
        this.sampleSet = sampleSet;
        this.sampleIndex = sampleIndex;
        this.volume = volume;
        this.uninherited = uninherited;
        this.effects = effects;
    }
}