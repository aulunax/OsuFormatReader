using System.Drawing;
using System.Globalization;
using OsuFormatReader.DataTypes;
using OsuFormatReader.Enums;
using OsuFormatReader.Interfaces;
using OsuFormatReader.Sections.HitObjectTypes;
using OsuFormatReader.Sections.HitObjectTypes.ObjectParamsTypes;

namespace OsuFormatReader.Parsers;

internal class HitObjectParser
{
    public static IHitObject? ParseHitObject(string value)
    {
        List<string> splitValue = ValueParser.ParseDelimitedStrings(value, 6);
        int x, y, time;
        HitSound hitSound;
        HitSample? hitSample;
        HitObjectType hitObjectType;
        string objParamsString = string.Empty;
        string hitSampleString = string.Empty;
        
       
        if (splitValue.Count == 6)
            ValueParser.SplitCommaSeparatedStringIntoLastAndRest(splitValue[5], out objParamsString,
                out hitSampleString);
        // Case: HitCircle with default HitSample
        else if (splitValue.Count == 5)
            objParamsString = string.Empty;
        else
            return null;

        
        
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
            
            // Handle HoldNote type for parsing
            if (hitObjectType.HasFlag(HitObjectType.HoldNote))
            {
                List<string> holdNoteEndingString = ValueParser.ParseDelimitedStrings(hitSampleString, 2, ':');
                objParamsString = holdNoteEndingString[0];
                if (holdNoteEndingString.Count == 2)
                    hitSampleString = holdNoteEndingString[1];
            }
            
            hitSample = ParseHitSample(hitSampleString);
        }
        else
        {
            return null;
        }

        // make a default HitSample if it is not implicitly given in the string
        if (hitSample is null)
        {
            hitSample = new HitSample();
            if (splitValue.Count > 5 && hitSampleString != string.Empty)
                objParamsString = splitValue[5];
        }

        IHitObject? newHitObject = null;

        if (hitObjectType.HasFlag(HitObjectType.HitCircle))
        {
            HitCircleParams hitCircleParams = new HitCircleParams();
            newHitObject = new HitCircle(x,y,time,hitObjectType,hitSound,hitCircleParams,hitSample);
        }
        else if (hitObjectType.HasFlag(HitObjectType.Slider))
        {
            SliderParams? sliderParams = ParseSliderParams(objParamsString);
            if (sliderParams is not null)
                newHitObject = new Slider(x,y,time,hitObjectType,hitSound,sliderParams,hitSample);
        }
        else if (hitObjectType.HasFlag(HitObjectType.Spinner))
        {
            SpinnerParams? spinnerParams = ParseSpinnerParams(objParamsString);
            if (spinnerParams is not null)
                newHitObject = new Spinner(x,y,time,hitObjectType,hitSound,spinnerParams,hitSample);
        }
        else if (hitObjectType.HasFlag(HitObjectType.HoldNote))
        {
            HoldParams? holdParams = ParseHoldParams(objParamsString);
            if (holdParams is not null)
                newHitObject = new Hold(x,y,time,hitObjectType,hitSound,holdParams,hitSample);
        }

        return newHitObject;
    }
    
    public static HitSample? ParseHitSample(string value)
    {
        List<string> parts = ValueParser.ParseDelimitedStrings(value, ':');
        
        // Allow only 2 parameters e.g.
        // https://osu.ppy.sh/beatmapsets/40826#taiko/174633
        if (parts.Count < 2)
            return null;
        
        if (parts.Count == 5 && 
            int.TryParse(parts[0], out int normalSet) &&
            int.TryParse(parts[1], out int additionSet) &&
            int.TryParse(parts[2], out int index) &&
            int.TryParse(parts[3], out int volume)
           )
        {
            return new HitSample(normalSet, additionSet, index, volume, parts[4]);
        }
        if (parts.Count == 3 && 
            int.TryParse(parts[0], out int normalSet2) &&
            int.TryParse(parts[1], out int additionSet2) &&
            int.TryParse(parts[2], out int index2)
           )
        {
            return new HitSample(normalSet2, additionSet2, index2);
        }
        if (parts.Count == 2 && 
            int.TryParse(parts[0], out int normalSet3) &&
            int.TryParse(parts[1], out int additionSet3)
           )
        {
            return new HitSample(normalSet3, additionSet3);
        }
        return null;
    }
    
    public static SliderParams? ParseSliderParams(string value)
    {
        List<string> parts = ValueParser.ParseDelimitedStrings(value, ',');

        if (parts.Count < 3)
            return null;

        int slides;
        double length;
        List<Point> curvePoints = new List<Point>();
        List<HitSound> edgeSounds = new List<HitSound>();
        List<EdgeSet> edgeSets = new List<EdgeSet>();
        
        
        List<string> curvePointsStringList = ValueParser.ParseDelimitedStrings(parts[0], '|');
        
        // curvePointsStringList also contains the character corresponding to the type of the slider curve,
        // so the list needs to have at least 1 element.
        // It doesn't have to contain any Points, because Aspire maps exist e.g.
        // https://osu.ppy.sh/beatmapsets/1219078#osu/2536330
        if (curvePointsStringList.Count == 0)
            return null;
        
        SliderCurveType curveType = (SliderCurveType)curvePointsStringList[0][0];
        curvePointsStringList.RemoveAt(0);
        
        foreach (string pointString in curvePointsStringList)
        {
            List<int> pointCoords = ValueParser.ParseDelimitedIntegers(pointString, ':');
            curvePoints.Add(new Point(pointCoords[0], pointCoords[1]));
        }
        
        if (!int.TryParse(parts[1], out slides) ||
            !double.TryParse(parts[2], CultureInfo.InvariantCulture, out length) 
           )
        {
            return null;
        }

        // edgeSounds and edgeSets appear to be optional (not mentioned in docs)
        // tested on Okaerinasai Azer set
        if (parts.Count == 5)
        {
            List<int> edgeSoundsIntegerList = ValueParser.ParseDelimitedIntegers(parts[3], '|');
            foreach (int edgeSound in edgeSoundsIntegerList)
            {
                edgeSounds.Add((HitSound)edgeSound);
            }
        
            List<string> edgeSetsStringList = ValueParser.ParseDelimitedStrings(parts[4], '|');
            foreach (string edgeSet in edgeSetsStringList)
            {
                List<int> setsValues = ValueParser.ParseDelimitedIntegers(edgeSet, ':');
                edgeSets.Add(new EdgeSet(setsValues[0], setsValues[1]));
            }
        }
        
        return new SliderParams(curveType, curvePoints, slides, length, edgeSounds, edgeSets);
    }
    
    public static SpinnerParams? ParseSpinnerParams(string value)
    {
        List<string> parts = ValueParser.ParseDelimitedStrings(value, ',');

        if (parts.Count != 1)
            return null;
        
        if (int.TryParse(parts[0], out int endTime) 
           )
        {
            return new SpinnerParams(endTime);
        }
        return null;
    }
    
    public static HoldParams? ParseHoldParams(string value)
    {
        List<string> parts = ValueParser.ParseDelimitedStrings(value, ',');

        if (parts.Count != 1)
            return null;
        
        if (int.TryParse(parts[0], out int endTime)
           )
        {
            return new HoldParams(endTime);
        }
        return null;
    }
}