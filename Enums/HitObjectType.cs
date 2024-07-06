namespace OsuFormatReader.Enums;

[Flags]
public enum HitObjectType : byte
{
    HitCircle = 1 << 0,
    Slider = 1 << 1,
    StartNewCombo = 1 << 2,
    Spinner = 1 << 3,
    ColourHaxHighDigit = 1 << 4,
    ColourHaxMidDigit = 1 << 5,
    ColourHaxLowDigit = 1 << 6,
    HoldNote = 1 << 7,
    ColourHaxMask = ColourHaxHighDigit | ColourHaxMidDigit | ColourHaxLowDigit,
    ObjectTypeMask = HitCircle | Slider | Spinner
}


