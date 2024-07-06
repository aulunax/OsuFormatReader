namespace OsuFormatReader.DataTypes;

public class HitSample
{
    public int normalSet { get; set; }
    public int additionSet  { get; set; }
    public int index { get; set; }
    public int volume { get; set; }
    public string filename { get; set; }
    
    public HitSample(int normalSet = 0, int additionSet = 0, int index = 0, int volume = 0, string filename = null)
    {
        this.normalSet = normalSet;
        this.additionSet = additionSet;
        this.index = index;
        this.volume = volume;
        this.filename = filename;
    }
}