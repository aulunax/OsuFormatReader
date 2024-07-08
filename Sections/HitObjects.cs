using OsuFormatReader.Enums;
using OsuFormatReader.Interfaces;
using OsuFormatReader.IO;
using OsuFormatReader.Parsers;

namespace OsuFormatReader.Sections;

public class HitObjects
{
    private readonly List<IHitObject> _hitObjects = new();

    public void AddHitObject(IHitObject hitObject)
    {
        _hitObjects.Add(hitObject);
    }

    public List<IHitObject> GetHitObjectList()
    {
        return _hitObjects;
    }

    public static HitObjects Read(OsuFormatStreamReader reader, HitObjects? outobj = null)
    {
        if (outobj is null)
            outobj = new HitObjects();
        
        reader.ReadUntilSection(SectionType.HitObjects);

        while (!reader.IsAtEnd && reader.SectionType == SectionType.HitObjects)
        {
            var line = reader.ReadParsedLine();

            if (line is null)
                continue;

            var newHitObject = HitObjectParser.ParseHitObject(line);

            if (newHitObject is null)
                continue;

            outobj.AddHitObject(newHitObject);
        }

        return outobj;
    }
}