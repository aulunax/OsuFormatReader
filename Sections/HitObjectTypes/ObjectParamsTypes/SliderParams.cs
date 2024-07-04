using System.Drawing;
using OsuFormatReader.DataTypes;
using OsuFormatReader.Enums;

namespace OsuFormatReader.Sections.HitObjectTypes.ObjectParamsTypes;

public class SliderParams
{
    public SliderCurveType curveType { get; set; }
    public List<Point> curvePoints  { get; set; }
    public int slides  { get; set; }
    public decimal length  { get; set; }
    public List<HitSound> edgeSounds  { get; set; }
    public List<EdgeSet> edgeSets  { get; set; }
}