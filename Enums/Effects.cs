namespace OsuFormatReader.Enums;

[Flags]
public enum Effects : byte
{
    KiaiTime = 1 << 0,
    FirstBarLineOmittion = 1 << 3
}