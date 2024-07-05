using System.Reflection;
using OsuFormatReader.Enums;
using OsuFormatReader.Utils;

namespace OsuFormatReader.IO;

public class KeyValueReader
{
    public static T? ReadAndUpdate<T>(OsuFormatReader reader, T outobj) where T : class
    {
        string? value;
        string? varName = reader.TryReadKeyValuePair(out value);
        
        PropertyInfo[] properties = typeof(T).GetProperties();

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
        
        Console.WriteLine($"Property Name: {property.Name}, Property Type: {property.PropertyType}");
        Console.WriteLine($"Property New Value: {value}");

        if (property.PropertyType == typeof(int))
            property.SetValue(outobj, int.Parse(value));
        else if (property.PropertyType == typeof(bool))
            property.SetValue(outobj, int.Parse(value) != 0);
        else if (property.PropertyType == typeof(string))
            property.SetValue(outobj, value);
        else if (property.PropertyType == typeof(decimal))
            property.SetValue(outobj, decimal.Parse(value.Replace('.',',')));  // TODO: Fix smth with Culture
        else if (property.PropertyType == typeof(List<int>)) 
            property.SetValue(outobj, ValueParser.ParseCommaSeparatedIntegers(value));
        else if (property.PropertyType == typeof(Colour))
            property.SetValue(outobj, ValueParser.ParseColour(value));
        else
            Console.WriteLine($"Unknown type\n");
        

        return outobj;
    }
}