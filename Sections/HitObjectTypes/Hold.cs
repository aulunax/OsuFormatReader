using OsuFormatReader.DataTypes;
using OsuFormatReader.Enums;
using OsuFormatReader.Interfaces;
using OsuFormatReader.Sections.HitObjectTypes.ObjectParamsTypes;

namespace OsuFormatReader.Sections.HitObjectTypes;

public class Hold : IHitObject<HoldParams>
{
    public const int DEFAULT_Y = 192;
    public int x { get; set; }
    public int y { get; set; }
    public int time { get; set; }
    public HitObjectType type { get; set; }
    public HitSound hitSound { get; set; }
    public HoldParams objectParams { get; set; }
    public HitSample hitSample { get; set; }

    public Hold(int x, int y, int time, HitObjectType type, HitSound hitSound, HoldParams objectParams, HitSample hitSample)
    {
        this.x = x;
        this.y = DEFAULT_Y;
        this.time = time;
        this.type = type;
        this.hitSound = hitSound;
        this.objectParams = objectParams;
        this.hitSample = hitSample;
    }
}