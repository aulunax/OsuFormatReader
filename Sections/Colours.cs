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
    
    public static void Read(OsuFormatReader reader, Colours outobj)
    {
       
    }
}