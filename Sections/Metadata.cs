﻿using OsuFormatReader.Enums;
using OsuFormatReader.IO;
using OsuFormatReader.Parsers;

namespace OsuFormatReader.Sections;

public class Metadata
{
    public string Title { get; set; }
    public string TitleUnicode { get; set; }
    public string Artist { get; set; }
    public string ArtistUnicode { get; set; }
    public string Creator { get; set; }
    public string Version { get; set; }
    public string Source { get; set; }
    public string Tags { get; set; }
    public int BeatmapID { get; set; }
    public int BeatmapSetID { get; set; }

    public List<string>? GetTagsAsList()
    {
        if (string.IsNullOrEmpty(Tags))
            return null;

        return Tags.Split(' ').ToList();
        ;
    }

    public static Metadata Read(OsuFormatStreamReader reader, Metadata? outobj = null)
    {
        if (outobj is null)
            outobj = new Metadata();

        reader.ReadUntilSection(SectionType.Metadata);

        while (!reader.IsAtEnd && reader.SectionType == SectionType.Metadata)
            KeyValueParser.ReadAndUpdateProperty(reader, outobj);

        return outobj;
    }
}