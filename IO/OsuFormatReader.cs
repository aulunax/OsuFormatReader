﻿namespace OsuFormatReader;

public class OsuFormatReader : IDisposable
{
    private readonly StreamReader _reader;
    public SectionType SectionType { get; private set; } = SectionType.None;

    public OsuFormatReader(Stream stream)
    {
        _reader = new StreamReader(stream);
    }

    public bool IsAtEnd { get; private set; } = false;

    public string? TryReadKeyValuePair(out string? value)
    {
        string? key = null;
        bool success = TryReadKeyValuePair<string>(out key, out value, s => s);
        value = success ? value : (string?)null;
        return key;
    }

    public string? ReadLine()
    {
        string? line = _reader.ReadLine();

        if (line is null)
        {
            IsAtEnd = true;
            return null;
        }

        line = line.Trim();
        
        if (line.StartsWith("//"))
        {
            line = null;
        }
        else if (line.StartsWith("["))
        {
            string sectionString = line.Substring(1,line.Length-2);
            SectionType = SectionTypeExtensions.ToSectionType(sectionString);
        }


        return line;
    }

    private bool TryReadKeyValuePair<T>(out string? key, out T? value, Func<string, T> parseFunc)
    {
        value = default;
        key = null;
        string? line = ReadLine();

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

    public void ReadUntilFirstSection()
    {
        while (SectionType == SectionType.None)
            ReadLine();
    }

    public void Dispose()
    {
        _reader.Dispose();
    }
}