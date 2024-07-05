using System.Data;
using System.Text.RegularExpressions;
using OsuFormatReader.Enums;
using OsuFormatReader.IO;
using OsuFormatReader.Parsers;

namespace OsuFormatReader.Sections;

public class Colours
{
    private static readonly Regex comboColourRegex = new Regex(@"^Combo(\d+)$");
    
    private Dictionary<int, Colour> _comboColoursDict = new Dictionary<int, Colour>();
    public Colour SliderTrackOverride = null;
    public Colour SliderBorder = null;

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
        if (!_comboColoursDict.TryAdd(comboNumber, colour))
        {
            _comboColoursDict[comboNumber] = colour;
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

            if (key == null || value == null)
                continue;

            if (key == "SliderTrackOverride" || key == "SliderBorder")
            {
                KeyValueReader.Update(key, value, outobj);
            }
            else if (comboColourRegex.IsMatch(key))
            {
                Match match = comboColourRegex.Match(key);
                string numberStr = match.Groups[1].Value;
                int number = int.Parse(numberStr);
                outobj.AddOrReplaceComboColour(number, ValueParser.ParseColour(value));
            }
        }

        return outobj;

    }
}