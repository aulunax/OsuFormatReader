using OsuFormatReader.Enums;
using OsuFormatReader.Parsers;

namespace OsuFormatReader.IO;

/// <summary>
///     Provides functionality for reading and parsing osu! format files from a stream.
///     This class reads the stream line by line and uses <see cref="OsuFormatParser"/> to parse the contents.
/// </summary>
public class OsuFormatStreamReader : IDisposable
{
    private readonly OsuFormatParser _parser;
    private readonly StreamReader _reader;

    /// <summary>
    ///     Initializes a new instance of the <see cref="OsuFormatStreamReader" /> class with the specified stream.
    /// </summary>
    /// <param name="stream">Stream from which <see cref="OsuFormatStreamReader" /> reads input.</param>
    public OsuFormatStreamReader(Stream stream)
    {
        _parser = new OsuFormatParser();
        _reader = new StreamReader(stream);
        CurrentLine = 0;
    }

    /// <summary>
    /// Line being currently read from stream
    /// </summary>
    internal int CurrentLine { get; private set; }

    /// <summary>
    ///     Version of the osu format of the file being read. <br />
    ///     Value of -1 means that reader was not able to read the version number properly.
    /// </summary>
    internal int FormatVersion => _parser.FormatVersion;

    /// <summary>
    ///     Section being currently parsed/read by the reader.
    /// </summary>
    internal SectionType SectionType => _parser.SectionType;

    /// <summary>
    ///     Value indicating whether the reader has reached the end of the stream.
    /// </summary>
    internal bool IsAtEnd { get; private set; }

    /// <summary>
    /// Tries to read a key-value pair from the reader.
    /// </summary>
    /// <param name="value">The value of the key-value pair, or null if read unsuccessfully.</param>
    /// <returns>Key string if read successfully, null otherwise</returns>
    internal string? TryReadKeyValuePair(out string? value)
    {
        string? key = _parser.TryReadKeyValuePair(this, out value);
        return key;
    }

    private string? ReadLine()
    {
        var line = _reader.ReadLine();

        if (line is null)
        {
            IsAtEnd = true;
            return null;
        }

        CurrentLine++;

        return line.Trim();
        ;
    }

    /// <summary>
    /// Reads the next parsed line from the stream.
    /// </summary>
    /// <returns>The next parsed line as a string, or null if the end of the stream is reached.</returns>
    internal string? ReadParsedLine()
    {
        var line = ReadLine();
        return _parser.ParseLine(line);
    }

    /// <summary>
    /// Reads from stream until reaching a valid section, or EOF.
    /// </summary>
    internal void ReadUntilFirstSection()
    {
        _parser.ReadUntilFirstSection(this);
    }


    /// <summary>
    /// Reads from stream until reaching a section specified by <see cref="sectionType"/>, or EOF.
    /// </summary>
    /// <param name="sectionType"><see cref="sectionType"/> reader reads to.</param>
    internal void ReadUntilSection(SectionType sectionType)
    {
        while (!IsAtEnd && SectionType != sectionType)
        {
            var line = ReadLine();
            _parser.ParseLine(line);
        }
    }

    /// <summary>
    /// Reports parsing error at current line with given message.
    /// </summary>
    /// <param name="message">Contains error message to display.</param>
    internal void ReportParserError(string message)
    {
        if (_reader.BaseStream is FileStream fileStream)
        {
            _parser.ReportError(message, CurrentLine, fileStream.Name);
            return;
        }

        _parser.ReportError(message, CurrentLine);
    }

    public void Dispose()
    {
        _reader.Dispose();
    }
}