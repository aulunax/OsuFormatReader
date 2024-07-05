using System.Diagnostics;
using System.Security.Cryptography;
using OsuFormatReader.DataTypes;
using OsuFormatReader.Sections;

namespace OsuFormatReader;

public class Test
{
    public static void Main()
    {
        using (OsuFormatReader reader = new OsuFormatReader(new FileStream("okaeri.osu", FileMode.Open)))
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            
            OsuFormat osuFormat = OsuFormat.Read(reader);
            
            stopwatch.Stop();
            TimeSpan timeTaken = stopwatch.Elapsed;
            string formattedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                timeTaken.Hours, timeTaken.Minutes, timeTaken.Seconds,
                timeTaken.Milliseconds);
            Console.WriteLine("Time taken: " + formattedTime);
        }
        
    }
}