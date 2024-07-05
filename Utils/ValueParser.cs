using OsuFormatReader.Enums;

namespace OsuFormatReader.Utils;

public class ValueParser
{
    public static List<int> ParseCommaSeparatedIntegers(string value)
    {
        var result = new List<int>();
        var parts = value.Split(',');

        foreach (var part in parts)
        {
            if (int.TryParse(part.Trim(), out int number))
            {
                result.Add(number);
            }
            else
            {
                throw new FormatException($"Unable to parse '{part}' as an integer.");
            }
        }

        return result;
    }

    public static Colour ParseColour(string value)
    {
        List<int> rgb = ParseCommaSeparatedIntegers(value);
        if (rgb.Count == 3)
            return new Colour(rgb[0], rgb[1], rgb[2]);
        throw new FormatException($"Unable to parse '{value}' as an 3 comma-separated integer values.");

    }
}