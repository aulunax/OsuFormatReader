using OsuFormatReader.DataTypes;
using OsuFormatReader.Enums;

namespace OsuFormatReader.Interfaces;

public interface IHitObject<TObjectParams> : IHitObject
{
    TObjectParams objectParams { get; set; }
    object IHitObject.objectParams
    {
        get => objectParams;
        set => objectParams = (TObjectParams)value;
    }
}