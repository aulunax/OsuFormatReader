using OsuFormatReader.Enums;

namespace OsuFormatReader.Sections;

public class Colours
{
    public Dictionary<int, Colour> ComboColours = new Dictionary<int, Colour>();
    public Colour SliderTrackOverride;
    public Colour SliderBorder;

    public void AddOrReplaceComboColour(int comboNumber, Colour colour)
    {
        if (!ComboColours.TryAdd(comboNumber, colour))
        {
            ComboColours[comboNumber] = colour;
        }
    }
    
    public static Colours Read(OsuFormatReader reader, Colours? outobj = null)
    {
        if (outobj is null)
            outobj = new Colours();

        while (!reader.IsAtEnd && reader.SectionType == SectionType.Colours)
        {
            string? value;
            string? key = reader.TryReadKeyValuePair(out value);
        }

        return outobj;

    }
}