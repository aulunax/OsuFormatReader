using OsuFormatReader.DataTypes;
using OsuFormatReader.Enums;

namespace OsuFormatReader.Interfaces;

public interface IHitObject
{
    int x { get; set; }
    int y { get; set; }
    int time { get; set; }
    HitObjectType type { get; set; }
    HitSound hitSound { get; set; }
    object objectParams { get; set; }
    HitSample hitSample { get; set; }
}