using OsuFormatReader.Interfaces;
using OsuFormatReader.Parsers;

namespace OsuFormatReader.Sections;

public class HitObjects
{
    private readonly List<IHitObject> _hitObjects = new List<IHitObject>();

    public void AddHitObject(IHitObject hitObject)
    {
        _hitObjects.Add(hitObject);
    }
    
    public List<IHitObject> GetHitObjectList()
    {
        return _hitObjects;
    }
    
    public static HitObjects Read(OsuFormatReader reader, HitObjects? outobj = null)
    {
        if (outobj is null)
            outobj = new HitObjects();

        while (!reader.IsAtEnd && reader.SectionType == SectionType.HitObjects)
        {
            string? line = reader.ReadLine();

            if (line is null)
                continue;
            
            IHitObject? newHitObject = HitObjectParser.ParseHitObject(line);
            
            if (newHitObject is null)
                continue;
            
            outobj.AddHitObject(newHitObject);
        }

        return outobj;
    }
}