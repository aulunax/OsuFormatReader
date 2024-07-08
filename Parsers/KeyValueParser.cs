using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using OsuFormatReader.Enums;
using OsuFormatReader.IO;

namespace OsuFormatReader.Parsers;

internal static class KeyValueParser
{
    public static T? ReadAndUpdateProperty<T>(OsuFormatStreamReader reader, T? outobj = null) where T : class, new()
    {
        if (outobj is null)
            outobj = new T();

        string? value;
        var varName = reader.TryReadKeyValuePair(out value);

        return UpdateProperty(varName, value, outobj);
    }

    public static T? UpdateProperty<T>(string? varName, string? value, T outobj) where T : class
    {
        var properties = typeof(T).GetProperties();

        PropertyInfo? property;
        try
        {
            property = properties.First(p => p.Name == varName);
        }
        catch (InvalidOperationException)
        {
            property = null;
        }

        if (varName is null || value is null || property is null)
            return null;

        if (property.PropertyType == typeof(int))
            property.SetValue(outobj, int.Parse(value));
        else if (property.PropertyType == typeof(bool))
            property.SetValue(outobj, int.Parse(value) != 0);
        else if (property.PropertyType == typeof(string))
            property.SetValue(outobj, value);
        else if (property.PropertyType == typeof(decimal))
            property.SetValue(outobj, decimal.Parse(value, CultureInfo.InvariantCulture));
        else if (property.PropertyType == typeof(List<int>))
            property.SetValue(outobj, ValueParser.ParseDelimitedIntegers(value));
        else if (property.PropertyType == typeof(Colour))
            property.SetValue(outobj, ValueParser.ParseColour(value));
        else
            Debug.WriteLine("Unknown type in KeyValueReader.Update\n");


        return outobj;
    }
}