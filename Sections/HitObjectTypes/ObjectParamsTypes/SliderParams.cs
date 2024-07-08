using System.Drawing;
using OsuFormatReader.DataTypes;
using OsuFormatReader.Enums;

namespace OsuFormatReader.Sections.HitObjectTypes.ObjectParamsTypes;

public class SliderParams
{
    public SliderParams(SliderCurveType curveType, List<Point> curvePoints, int slides, double length,
        List<HitSound> edgeSounds, List<EdgeSet> edgeSets)
    {
        this.curveType = curveType;
        this.curvePoints = curvePoints;
        this.slides = slides;
        this.length = length;
        this.edgeSounds = edgeSounds;
        this.edgeSets = edgeSets;
    }

    public SliderCurveType curveType { get; set; }
    public List<Point> curvePoints { get; set; }
    public int slides { get; set; }

    /// <summary>
    ///     Length of the slider in osu pixels <br />
    ///     Chosen double as type, because this exists:
    ///     https://osu.ppy.sh/beatmapsets/478093#osu/1029976
    /// </summary>
    public double length { get; set; }

    public List<HitSound> edgeSounds { get; set; }
    public List<EdgeSet> edgeSets { get; set; }
}