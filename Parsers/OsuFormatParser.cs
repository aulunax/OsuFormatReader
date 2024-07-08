using System.Text.RegularExpressions;
using OsuFormatReader.Enums;
using OsuFormatReader.IO;

namespace OsuFormatReader.Parsers;

internal class OsuFormatParser
{
    private static readonly Regex VersionRegex = new(@"^osu file format v([\d]+)");

    
    /// <summary>
    ///     Version of the osu format of the file being read. <br />
    ///     Value of -1 means that reader was not able to read the version number properly.
    /// </summary>
    internal int FormatVersion { get; private set; }
    
    /// <summary>
    /// Section being currently parsed/read by the parser.
    /// </summary>
    internal SectionType SectionType { get; private set; } = SectionType.None;
    
    /// <summary>
    /// Parses a line to determine its type and updates the section if necessary.
    /// Also ignores comments made with double slash.
    /// </summary>
    /// <param name="line">The line to parse.</param>
    /// <returns>The parsed line, or null if the line is a comment or empty.</returns>
    internal string? ParseLine(string? line)
    {
        if (line is null)
            return null;

        if (line.StartsWith("//"))
        {
            return null;
        }
        else if (line.StartsWith("["))
        {
            var sectionString = line.Substring(1, line.Length - 2);
            SectionType = SectionTypeExtensions.ToSectionType(sectionString);
        }

        return line;
    }
    
    
    /// <summary>
    /// Tries to read a key-value pair from the reader.
    /// </summary>
    /// <param name="reader">The <see cref="OsuFormatStreamReader"/> instance.</param>
    /// <param name="value">The value of the key-value pair, or null if read unsuccessfully.</param>
    /// <returns>Key string if read successfully, null otherwise</returns>
    internal string? TryReadKeyValuePair(OsuFormatStreamReader reader, out string? value)
    {
        string? key = null;
        var success = TryReadKeyValuePair<string>(reader, out key, out value, s => s);
        value = success ? value : null;
        return key;
    }

    /// <summary>
    /// Tries to read a key-value pair from the reader.
    /// </summary>
    /// <param name="reader">The <see cref="OsuFormatStreamReader"/> instance.</param>
    /// <param name="key">The key of the key-value pair.</param>
    /// <param name="value">The value of the key-value pair.</param>
    /// <param name="parseFunc">Function to parse from string to type T</param>
    /// <returns>True if a key-value pair is successfully read; otherwise, false.</returns>
    private bool TryReadKeyValuePair<T>(OsuFormatStreamReader reader, out string? key, out T? value, Func<string, T> parseFunc)
    {
        value = default;
        key = null;
        var line = reader.ReadParsedLine();

        if (line is null)
            return false;

        var parts = line.Split([':'], 2);
        if (parts.Length == 2)
        {
            key = parts[0].Trim();
            value = parseFunc(parts[1].Trim());
            return true;
        }

        return false;
    }

    /// <summary>
    /// Reads until the first section is found.
    /// </summary>
    /// <param name="reader">The <see cref="OsuFormatStreamReader"/> instance.</param>
    internal void ReadUntilFirstSection(OsuFormatStreamReader reader)
    {
        while (SectionType == SectionType.None)
        {
            string? line = reader.ReadParsedLine();
            if (line is null)
                return;

            Match match = VersionRegex.Match(line);
            if (match.Success && match.Groups.Count == 2)
            {
                if (int.TryParse(match.Groups[1].ToString(), out int version))
                    FormatVersion = version;
                else
                    FormatVersion = -1;
            }
        }
    }
}