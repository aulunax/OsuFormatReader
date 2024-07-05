using OsuFormatReader.Enums;
using OsuFormatReader.Interfaces;
using OsuFormatReader.Sections.EventTypes;

namespace OsuFormatReader.Parsers;

public class ValueParser
{
    /// <summary>
    /// This method splits a string into integers based on delimiting commas and returns list of those integers.
    /// </summary>
    /// <param name="value">String of comma separated integers</param>
    /// <returns>List of integers</returns>
    /// <exception cref="FormatException">a substring couldn't be parsed as an integer</exception>
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
    
    /// <summary>
    /// This method splits a string into substrings based on delimiting commas and returns list of those substrings. 
    /// </summary>
    /// <param name="value">String of comma separated strings</param>
    /// <returns>List of substrings</returns>
    public static List<string> ParseCommaSeparatedStrings(string value)
    {
        var result = new List<string>();
        var parts = value.Split(',');

        foreach (var part in parts)
        {
            result.Add(part);
        }

        return result;
    }
    
    /// <summary>
    /// This method splits a string into a specified number of substrings based on
    /// delimiting commas and returns list of those substrings. 
    /// </summary>
    /// <param name="value">String of comma separated strings</param>
    /// <param name="count">Max number of substrings to return</param>
    /// <returns>List of substrings</returns>
    public static List<string> ParseCommaSeparatedStrings(string value, int count)
    {
        var result = new List<string>();
        var parts = value.Split([','],count);

        foreach (var part in parts)
        {
            result.Add(part);
        }

        return result;
    }

    /// <summary>
    /// Given a string of 3 comma separated integers, returns Colour object, in which Red, Green and Blue properties are
    /// accordingly assigned as first, second and third comma separated integer.
    /// </summary>
    /// <param name="value">String containing 3 comma separated integers</param>
    /// <returns>Colour object</returns>
    /// <exception cref="FormatException">value count is not 3</exception>
    public static Colour ParseColour(string value)
    {
        List<int> rgb = ParseCommaSeparatedIntegers(value);
        if (rgb.Count == 3)
            return new Colour(rgb[0], rgb[1], rgb[2]);
        throw new FormatException($"Unable to parse '{value}' as an 3 comma-separated integer values.");

    }
}