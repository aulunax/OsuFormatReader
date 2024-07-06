using OsuFormatReader.Enums;

namespace OsuFormatReader.DataTypes;

public class TimingPoint
{
    public int time { get; set; }
    public decimal beatLength  { get; set; }
    public int meter  { get; set; }
    public int sampleSet  { get; set; }
    public int sampleIndex  { get; set; }
    public int volume  { get; set; }
    public bool uninherited  { get; set; }
    public Effects effects  { get; set; }

    public TimingPoint() { }

    public TimingPoint(int time, decimal beatLength, int meter, int sampleSet, int sampleIndex, int volume, bool uninherited, Effects effects)
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