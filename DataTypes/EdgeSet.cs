namespace OsuFormatReader.DataTypes;

public class EdgeSet
{
    public EdgeSet(int normalSet, int additionSet)
    {
        this.normalSet = normalSet;
        this.additionSet = additionSet;
    }

    public int normalSet { get; set; }
    public int additionSet { get; set; }
}