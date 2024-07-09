using System.Text.RegularExpressions;
using OsuFormatReader.Enums;
using OsuFormatReader.IO;
using OsuFormatReader.Parsers;

namespace OsuFormatReader.Sections;

public class Colours
{
    private static readonly Regex comboColourRegex = new(@"^Combo(\d+)$");

    private readonly Dictionary<int, Colour> _comboColoursDict = new();
    public Colour SliderBorder { get; set; } = null;
    public Colour SliderTrackOverride { get; set; } = null;

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

            if (string.Equals(key, "SliderTrackOverride", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(key, "SliderBorder", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    KeyValueParser.UpdateProperty(key, value, outobj);
                }
                catch (FormatException e)
                {
                    reader.ReportParserError(e.Message);
                }
            }
            else if (comboColourRegex.IsMatch(key))
            {
                var match = comboColourRegex.Match(key);
                var numberStr = match.Groups[1].Value;
                if (int.TryParse(numberStr, out int number))
                {
                    try
                    {
                        outobj.AddOrReplaceComboColour(number, ValueParser.ParseColour(value));
                    }
                    catch (FormatException e)
                    {
                        reader.ReportParserError(e.Message);
                    }
                }
                else
                {
                    reader.ReportParserError($"Invalid combo number in string \"{key}\"");
                }
            }
            else
            {
                reader.ReportParserError($"Invalid key string \"{key}\"");
            }
        }

        return outobj;
    }
}