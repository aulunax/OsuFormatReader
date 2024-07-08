using System.Text.RegularExpressions;
using OsuFormatReader.Enums;
using OsuFormatReader.IO;
using OsuFormatReader.Parsers;

namespace OsuFormatReader.Sections;

public class Colours
{
    private static readonly Regex comboColourRegex = new(@"^Combo(\d+)$");

    private readonly Dictionary<int, Colour> _comboColoursDict = new();
    public Colour SliderBorder = null;
    public Colour SliderTrackOverride = null;

    public Colour GetComboColour(int index)
    {
        return _comboColoursDict[index];
    }

    public Dictionary<int, Colour> GetComboColourDictionary(int index)
    {
        return _comboColoursDict;
    }

    public void AddOrReplaceComboColour(int comboNumber, Colour colour)
    {
        if (!_comboColoursDict.TryAdd(comboNumber, colour)) _comboColoursDict[comboNumber] = colour;
    }

    public static Colours Read(OsuFormatStreamReader reader, Colours? outobj = null)
    {
        if (outobj is null)
            outobj = new Colours();

        reader.ReadUntilSection(SectionType.Colours);
        
        while (!reader.IsAtEnd && reader.SectionType == SectionType.Colours)
        {
            string? value;
            var key = reader.TryReadKeyValuePair(out value);

            if (key == null || value == null)
                continue;

            if (key == "SliderTrackOverride" || key == "SliderBorder")
            {
                KeyValueParser.UpdateProperty(key, value, outobj);
            }
            else if (comboColourRegex.IsMatch(key))
            {
                var match = comboColourRegex.Match(key);
                var numberStr = match.Groups[1].Value;
                var number = int.Parse(numberStr);
                outobj.AddOrReplaceComboColour(number, ValueParser.ParseColour(value));
            }
        }

        return outobj;
    }
}