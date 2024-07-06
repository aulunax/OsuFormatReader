using OsuFormatReader.DataTypes;
using OsuFormatReader.Enums;
using OsuFormatReader.Interfaces;
using OsuFormatReader.Sections.HitObjectTypes;
using OsuFormatReader.Sections.HitObjectTypes.ObjectParamsTypes;

namespace OsuFormatReader.Parsers;

public class HitObjectParser
{
    public static IHitObject? ParseHitObject(string value)
    {
        List<string> splitValue = ValueParser.ParseDelimitedStrings(value, 6);
        int x, y, time;
        HitSound hitSound;
        HitSample? hitSample;
        HitObjectType hitObjectType;

        ValueParser.SplitCommaSeparatedStringIntoLastAndRest(splitValue[5], out string objParamsString,
            out string hitSampleString);

        
        
        if (
            int.TryParse(splitValue[0], out x) &&
            int.TryParse(splitValue[1], out y) &&
            int.TryParse(splitValue[2], out time) &&
            int.TryParse(splitValue[3], out int typeInt) &&
            int.TryParse(splitValue[4], out int hitSoundInt)
        )
        {
            hitObjectType = (HitObjectType)typeInt;
            hitSound = (HitSound)hitSoundInt;
            hitSample = ParseHitSample(hitSampleString);
        }
        else
        {
            return null;
        }

        if (hitSample is null)
        {
            hitSample = new HitSample();
            objParamsString = splitValue[5];
        }

        IHitObject? newHitObject = null;

        if (hitObjectType.HasFlag(HitObjectType.HitCircle))
        {
            newHitObject = new HitCircle(x,y,time,hitObjectType,hitSound,new HitCircleParams(),hitSample);
        }
        else if (hitObjectType.HasFlag(HitObjectType.Slider))
        {
            newHitObject = new Slider(x,y,time,hitObjectType,hitSound,new SliderParams(),hitSample);
        }
        else if (hitObjectType.HasFlag(HitObjectType.Spinner))
        {
            newHitObject = new Spinner(x,y,time,hitObjectType,hitSound,new SpinnerParams(),hitSample);
        }
        else if (hitObjectType.HasFlag(HitObjectType.HoldNote))
        {
            newHitObject = new Hold(x,y,time,hitObjectType,hitSound,new HoldParams(),hitSample);
        }

        return newHitObject;
    }
    
    public static HitSample? ParseHitSample(string value)
    {
        List<string> parts = ValueParser.ParseDelimitedStrings(value, ':');

        if (parts.Count != 5)
            return null;
        
        if (int.TryParse(parts[0], out int normalSet) &&
            int.TryParse(parts[1], out int additionSet) &&
            int.TryParse(parts[2], out int index) &&
            int.TryParse(parts[3], out int volume)
           )
        {
            return new HitSample(normalSet, additionSet, index, volume, parts[4]);
        }
        return null;
    }
}