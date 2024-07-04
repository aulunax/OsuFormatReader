using OsuFormatReader.Interfaces;

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
    
    public static void Read(OsuFormatReader reader, HitObjects outobj)
    {
       
    }
}