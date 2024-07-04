namespace OsuFormatReader.DataTypes;

public class EdgeSet
{
    public int normalSet { get; set; }
    public int additionSet  { get; set; }

    public EdgeSet(int normalSet, int additionSet)
    {
        this.normalSet = normalSet;
        this.additionSet = additionSet;
    }
}