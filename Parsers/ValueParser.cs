using System.Globalization;
using OsuFormatReader.DataTypes;
using OsuFormatReader.Enums;

namespace OsuFormatReader.Parsers;

internal static class ValueParser
{
    /// <summary>
    ///     This method splits a string into integers based on the specified delimiter and returns a list of those integers.
    /// </summary>
    /// <param name="value">String of delimited integers.</param>
    /// <param name="delimiter">The character used to delimit the input string.</param>
    /// <returns>List of integers.</returns>
    /// <exception cref="FormatException">A substring couldn't be parsed as an integer.</exception>
    public static List<int> ParseDelimitedIntegers(string value, char delimiter = ',')
    {
        var result = new List<int>();
        var parts = value.Split(delimiter);

        foreach (var part in parts)
            if (part == string.Empty)
                continue;
            else if (int.TryParse(part.Trim(), out var number))
                result.Add(number);
            else
                throw new FormatException(
                    $"Unable to parse '{part}' from '{parts}' of size '{parts.Length}' as an integer.");

        return result;
    }

    /// <summary>
    ///     This method splits a string into substrings based on the specified delimiter and returns a list of those
    ///     substrings.
    /// </summary>
    /// <param name="value">String of delimited substrings.</param>
    /// <param name="delimiter">The character used to delimit the input string.</param>
    /// <returns>List of substrings.</returns>
    public static List<string> ParseDelimitedStrings(string value, char delimiter = ',')
    {
        var result = new List<string>();
        var parts = value.Split(delimiter);

        foreach (var part in parts) result.Add(part);

        return result;
    }

    /// <summary>
    ///     This method splits a string into a specified number of substrings based on the specified delimiter and returns a
    ///     list of those substrings.
    /// </summary>
    /// <param name="value">String of delimited substrings.</param>
    /// <param name="count">Max number of substrings to return.</param>
    /// <param name="delimiter">The character used to delimit the input string.</param>
    /// <returns>List of substrings.</returns>
    public static List<string> ParseDelimitedStrings(string value, int count, char delimiter = ',')
    {
        var result = new List<string>();
        var parts = value.Split([delimiter], count);

        foreach (var part in parts) result.Add(part);

        return result;
    }

    /// <summary>
    ///     Given a string of 3 comma separated integers, returns Colour object, in which Red, Green and Blue properties are
    ///     accordingly assigned as first, second and third comma separated integer.
    /// </summary>
    /// <param name="value">String containing 3 comma separated integers</param>
    /// <returns>Colour object</returns>
    /// <exception cref="FormatException">value count is not 3</exception>
    public static Colour ParseColour(string value)
    {
        var rgb = ParseDelimitedIntegers(value);
        if (rgb.Count == 3)
            return new Colour(rgb[0], rgb[1], rgb[2]);
        throw new FormatException($"Unable to parse '{value}' as an 3 comma-separated integer values.");
    }


    /// <summary>
    ///     Given a string of comma separated substrings, tries to return a TimingPoint object, which properties are
    ///     assigned accordingly to values parsed from the substrings.
    /// </summary>
    /// <param name="value">String containing comma separated substrings, in TimingPoint format</param>
    /// <param name="formatVersion">Version of .osu file format</param>
    /// <returns>TimingPoint object on success, null on failure</returns>
    public static TimingPoint? ParseTimingPoint(string value, int formatVersion = 14)
    {
        var parts = ParseDelimitedStrings(value);


        if ((formatVersion >= 6 && parts.Count != 8) ||
            (formatVersion == 5 && parts.Count != 7) ||
            (formatVersion == 4 && parts.Count != 6) ||
            (formatVersion <= 3 && parts.Count != 2))
            return null;

        double time;
        double beatLength;
        var meter = 4;
        var sampleSet = 0;
        var sampleIndex = 0;
        var volume = 0;
        var uninherited = 1;
        var effects = 0;

        // time as double is allowed i guess e.g.
        // https://osu.ppy.sh/beatmapsets/107763#osu/282251
        if (double.TryParse(parts[0], CultureInfo.InvariantCulture, out time) &&
            double.TryParse(parts[1], CultureInfo.InvariantCulture, out beatLength))
        {
            if (formatVersion >= 4)
                if (!int.TryParse(parts[2], out meter) ||
                    !int.TryParse(parts[3], out sampleSet) ||
                    !int.TryParse(parts[4], out sampleIndex) ||
                    !int.TryParse(parts[5], out volume))
                    return null;

            if (formatVersion >= 5)
                if (!int.TryParse(parts[6], out uninherited))
                    return null;

            if (formatVersion >= 6)
                if (!int.TryParse(parts[7], out effects))
                    return null;

            return new TimingPoint((int)time, beatLength, meter, sampleSet, sampleIndex, volume, uninherited != 0,
                (Effects)effects);
        }

        return null;
    }


    /// <summary>
    ///     Splits a comma-separated string into the last value and the rest of the string.
    /// </summary>
    /// <param name="input">The input comma-separated string.</param>
    /// <param name="rest">The substring containing all values except the last one.</param>
    /// <param name="last">The last value in the comma-separated string.</param>
    public static void SplitCommaSeparatedStringIntoLastAndRest(string input, out string rest, out string last)
    {
        var lastCommaIndex = input.LastIndexOf(',');

        if (lastCommaIndex == -1)
        {
            rest = string.Empty;
            last = input;
        }
        else
        {
            rest = input.Substring(0, lastCommaIndex);
            last = input.Substring(lastCommaIndex + 1);
        }
    }
}