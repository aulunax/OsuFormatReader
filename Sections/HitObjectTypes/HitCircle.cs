using OsuFormatReader.DataTypes;
using OsuFormatReader.Enums;
using OsuFormatReader.Interfaces;
using OsuFormatReader.Sections.HitObjectTypes.ObjectParamsTypes;

namespace OsuFormatReader.Sections.HitObjectTypes;

public class HitCircle : IHitObject<HitCircleParams>
{
    public HitCircle(int x, int y, int time, HitObjectType type, HitSound hitSound, HitCircleParams objectParams,
        HitSample hitSample)
    {
        this.x = x;
        this.y = y;
        this.time = time;
        this.type = type;
        this.hitSound = hitSound;
        this.objectParams = objectParams;
        this.hitSample = hitSample;
    }

    public int x { get; set; }
    public int y { get; set; }
    public int time { get; set; }
    public HitObjectType type { get; set; }
    public HitSound hitSound { get; set; }
    public HitCircleParams objectParams { get; set; }
    public HitSample hitSample { get; set; }
}