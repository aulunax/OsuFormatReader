using System.Drawing;
using System.Globalization;
using OsuFormatReader.DataTypes;
using OsuFormatReader.Enums;
using OsuFormatReader.Interfaces;
using OsuFormatReader.Sections.HitObjectTypes;
using OsuFormatReader.Sections.HitObjectTypes.ObjectParamsTypes;

namespace OsuFormatReader.Parsers;

internal static class HitObjectParser
{
    public static IHitObject? ParseHitObject(string value)
    {
        var splitValue = ValueParser.ParseDelimitedStrings(value, 6);
        int x, y, time;
        HitSound hitSound;
        HitSample? hitSample;
        HitObjectType hitObjectType;
        var objParamsString = string.Empty;
        var hitSampleString = string.Empty;


        if (splitValue.Count == 6)
            ValueParser.SplitCommaSeparatedStringIntoLastAndRest(splitValue[5], out objParamsString,
                out hitSampleString);
        // Case: HitCircle with default HitSample
        else if (splitValue.Count == 5)
            objParamsString = string.Empty;
        else
            throw new FormatException($"Expected at least 5 comma separated values in '{value}'");


        if (
            int.TryParse(splitValue[0], out x) &&
            int.TryParse(splitValue[1], out y) &&
            int.TryParse(splitValue[2], out time) &&
            int.TryParse(splitValue[3], out var typeInt) &&
            int.TryParse(splitValue[4], out var hitSoundInt)
        )
        {
            hitObjectType = (HitObjectType)typeInt;
            hitSound = (HitSound)hitSoundInt;

            // Handle HoldNote type for parsing
            if (hitObjectType.HasFlag(HitObjectType.HoldNote))
            {
                var holdNoteEndingString = ValueParser.ParseDelimitedStrings(hitSampleString, 2, ':');
                objParamsString = holdNoteEndingString[0];
                if (holdNoteEndingString.Count == 2)
                    hitSampleString = holdNoteEndingString[1];
            }

            hitSample = ParseHitSample(hitSampleString);
        }
        else
        {
            throw new FormatException($"Expected [int, int, int, int, int, ...] in '{value}'");
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
            var hitCircleParams = new HitCircleParams();
            newHitObject = new HitCircle(x, y, time, hitObjectType, hitSound, hitCircleParams, hitSample);
        }
        else if (hitObjectType.HasFlag(HitObjectType.Slider))
        {
            var sliderParams = ParseSliderParams(objParamsString);
            if (sliderParams is not null)
                newHitObject = new Slider(x, y, time, hitObjectType, hitSound, sliderParams, hitSample);
        }
        else if (hitObjectType.HasFlag(HitObjectType.Spinner))
        {
            var spinnerParams = ParseSpinnerParams(objParamsString);
            if (spinnerParams is not null)
                newHitObject = new Spinner(x, y, time, hitObjectType, hitSound, spinnerParams, hitSample);
        }
        else if (hitObjectType.HasFlag(HitObjectType.HoldNote))
        {
            var holdParams = ParseHoldParams(objParamsString);
            if (holdParams is not null)
                newHitObject = new Hold(x, y, time, hitObjectType, hitSound, holdParams, hitSample);
        }

        return newHitObject;
    }

    public static HitSample? ParseHitSample(string value)
    {
        var parts = ValueParser.ParseDelimitedStrings(value, ':');

        // Allow as little as 2 parameters e.g.
        // https://osu.ppy.sh/beatmapsets/40826#taiko/174633
        if (parts.Count < 2)
            return null;

        if (parts.Count == 5 &&
            int.TryParse(parts[0], out var normalSet) &&
            int.TryParse(parts[1], out var additionSet) &&
            int.TryParse(parts[2], out var index) &&
            int.TryParse(parts[3], out var volume)
           )
            return new HitSample(normalSet, additionSet, index, volume, parts[4]);
        if (parts.Count == 3 &&
            int.TryParse(parts[0], out var normalSet2) &&
            int.TryParse(parts[1], out var additionSet2) &&
            int.TryParse(parts[2], out var index2)
           )
            return new HitSample(normalSet2, additionSet2, index2);
        if (parts.Count == 2 &&
            int.TryParse(parts[0], out var normalSet3) &&
            int.TryParse(parts[1], out var additionSet3)
           )
            return new HitSample(normalSet3, additionSet3);
        throw new FormatException($"HitSample: Invalid string '{value}'; expected 0, 2, 3 or 5 colon separated values");
    }

    public static SliderParams? ParseSliderParams(string value)
    {
        var parts = ValueParser.ParseDelimitedStrings(value);

        if (parts.Count < 3)
            throw new FormatException($"SliderParams: Expected at least 3 comma separated values in '{value}'");

        int slides;
        double length;
        
        var curvePointsStringList = ValueParser.ParseDelimitedStrings(parts[0], '|');
        
        var curvePoints = new List<Point>(parts.Count - 1);
        List<HitSound> edgeSounds = new List<HitSound>();
        List<EdgeSet> edgeSets = new List<EdgeSet>();
        
        // curvePointsStringList also contains the character corresponding to the type of the slider curve,
        // so the list needs to have at least 1 element.
        // It doesn't have to contain any Points, because Aspire maps exist e.g.
        // https://osu.ppy.sh/beatmapsets/1219078#osu/2536330
        if (curvePointsStringList.Count == 0)
            throw new FormatException($"SliderParams: Slider must have a specified type");

        var curveType = (SliderCurveType)curvePointsStringList[0][0];
        curvePointsStringList.RemoveAt(0);

        foreach (var pointString in curvePointsStringList)
        {
            var pointCoords = ValueParser.ParseDelimitedIntegers(pointString, ':');
            curvePoints.Add(new Point(pointCoords[0], pointCoords[1]));
        }

        if (!int.TryParse(parts[1], out slides) ||
            !double.TryParse(parts[2], CultureInfo.InvariantCulture, out length)
           )
            throw new FormatException($"SliderParams: Expected [int, double] in '{parts[1] + "," + parts[2]}'");


        // edgeSounds and edgeSets appear to be optional (not mentioned in docs)
        // tested on Okaerinasai Azer set
        if (parts.Count == 5)
        {
            edgeSounds.Capacity = parts[3].Length;
            edgeSets.Capacity = parts[4].Length;
            
            var edgeSoundsIntegerList = ValueParser.ParseDelimitedIntegers(parts[3], '|');
            foreach (var edgeSound in edgeSoundsIntegerList) edgeSounds.Add((HitSound)edgeSound);

            var edgeSetsStringList = ValueParser.ParseDelimitedStrings(parts[4], '|');
            foreach (var edgeSet in edgeSetsStringList)
            {
                var setsValues = ValueParser.ParseDelimitedIntegers(edgeSet, ':');
                edgeSets.Add(new EdgeSet(setsValues[0], setsValues[1]));
            }
        }

        return new SliderParams(curveType, curvePoints, slides, length, edgeSounds, edgeSets);
    }

    public static SpinnerParams? ParseSpinnerParams(string value)
    {
        var parts = ValueParser.ParseDelimitedStrings(value);

        if (parts.Count != 1)
            throw new FormatException($"SpinnerParams: Expected singular value in '{value}'");

        if (int.TryParse(parts[0], out var endTime)
           )
            return new SpinnerParams(endTime);
        throw new FormatException($"SpinnerParams: Expected int value at '{value}'");
    }

    public static HoldParams? ParseHoldParams(string value)
    {
        var parts = ValueParser.ParseDelimitedStrings(value);

        if (parts.Count != 1)
            throw new FormatException($"HoldParams: Expected singular value in '{value}'");

        if (int.TryParse(parts[0], out var endTime)
           )
            return new HoldParams(endTime);
        throw new FormatException($"HoldParams: Expected int value at '{value}'");
    }
}