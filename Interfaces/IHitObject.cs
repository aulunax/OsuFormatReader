using OsuFormatReader.DataTypes;
using OsuFormatReader.Enums;

namespace OsuFormatReader.Interfaces;

public interface IHitObject<TObjectParams> : IHitObject
{
    int x { get; set; }
    int y { get; set; }
    int time { get; set; }
    HitObjectType type { get; set; }
    HitSound hitSound { get; set; }
    TObjectParams objectParams { get; set; }
    HitSample hitSample { get; set; }
    
    object IHitObject.objectParams
    {
        get => objectParams;
        set => objectParams = (TObjectParams)value;
    }
}