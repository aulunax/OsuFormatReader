using System.Collections.Concurrent;
using System.Diagnostics;
using System.Security.Cryptography;
using OsuFormatReader.DataTypes;
using OsuFormatReader.Enums;
using OsuFormatReader.Sections;

namespace OsuFormatReader;

internal class Test
{
    private static List<string> GetOsuFiles(string folderPath)
    {
        List<string> osuFiles = new List<string>();
        try
        {
            // Search recursively for all .osu files
            osuFiles.AddRange(Directory.GetFiles(folderPath, "*.osu", SearchOption.AllDirectories));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while searching for .osu files: {ex.Message}");
        }

        return osuFiles;
    }

    private static void ParallelMain()
    {
        List<string> filePaths = GetOsuFiles("Insert path here");
        ConcurrentBag<OsuFormat> osuFormats = new ConcurrentBag<OsuFormat>();
        ConcurrentBag<TimeSpan> timeSpans = new ConcurrentBag<TimeSpan>();

        int totalFiles = filePaths.Count;
        int processedFiles = 0;
        
        Stopwatch totalTime = new Stopwatch();
        totalTime.Start();
        
        Parallel.ForEach(filePaths, filePath =>
        {
            using (OsuFormatReader reader = new OsuFormatReader(new FileStream(filePath, FileMode.Open)))
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
            
                OsuFormat osuFormat = OsuFormat.Read(reader);
            
                stopwatch.Stop();
                TimeSpan timeTaken = stopwatch.Elapsed;
                timeSpans.Add(timeTaken);
                string formattedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                    timeTaken.Hours, timeTaken.Minutes, timeTaken.Seconds,
                    timeTaken.Milliseconds);
                
                osuFormats.Add(osuFormat);
                Console.WriteLine(osuFormat.Metadata.Artist + " - " + osuFormat.Metadata.Title + " [" + osuFormat.Metadata.Version + "]");
                Console.WriteLine("Beatmapset ID: " + osuFormat.Metadata.BeatmapSetID );
                Console.WriteLine("Beatmap ID: " + osuFormat.Metadata.BeatmapID);
                // Console.WriteLine("Circles: " + osuFormat.HitObjects.GetHitObjectList().Count(o => o.type.HasFlag(HitObjectType.HitCircle)));
                // Console.WriteLine("Sliders: " + osuFormat.HitObjects.GetHitObjectList().Count(o => o.type.HasFlag(HitObjectType.Slider)));
                // Console.WriteLine("Spinners: " + osuFormat.HitObjects.GetHitObjectList().Count(o => o.type.HasFlag(HitObjectType.Spinner)));

                Console.WriteLine("Time taken: " + formattedTime);

                int completed = Interlocked.Increment(ref processedFiles);
                Console.WriteLine("Completed: " + completed + "/" + totalFiles + "\n");
            }
        });
        
        totalTime.Stop();
        TimeSpan totalTimeElapsed = totalTime.Elapsed;
        string formattedTotalTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
            totalTimeElapsed.Hours, totalTimeElapsed.Minutes, totalTimeElapsed.Seconds,
            totalTimeElapsed.Milliseconds);
        Console.WriteLine("Total time taken: " + formattedTotalTime);
    }
    
    
    public static void Main()
    {
        List<string> filePaths = GetOsuFiles("Insert path here");
        List<OsuFormat> osuFormats = new List<OsuFormat>();
        List<TimeSpan> timeSpans = new List<TimeSpan>();

        int processedFiles = 0;
        
        Stopwatch totalTime = new Stopwatch();
        totalTime.Start();
        
        foreach (var filePath in filePaths)
        {
            processedFiles++;
            using (OsuFormatReader reader = new OsuFormatReader(new FileStream(filePath, FileMode.Open)))
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
            
                OsuFormat osuFormat = OsuFormat.Read(reader);
            
                stopwatch.Stop();
                TimeSpan timeTaken = stopwatch.Elapsed;
                timeSpans.Add(timeTaken);
                string formattedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                    timeTaken.Hours, timeTaken.Minutes, timeTaken.Seconds,
                    timeTaken.Milliseconds);
                
                osuFormats.Add(osuFormat);
                Console.WriteLine(osuFormat.Metadata.Artist + " - " + osuFormat.Metadata.Title + " [" + osuFormat.Metadata.Version + "]");
                Console.WriteLine("Beatmapset ID: " + osuFormat.Metadata.BeatmapSetID );
                Console.WriteLine("Beatmap ID: " + osuFormat.Metadata.BeatmapID);
                // Console.WriteLine("Circles: " + osuFormat.HitObjects.GetHitObjectList().Count(o => o.type.HasFlag(HitObjectType.HitCircle)));
                // Console.WriteLine("Sliders: " + osuFormat.HitObjects.GetHitObjectList().Count(o => o.type.HasFlag(HitObjectType.Slider)));
                // Console.WriteLine("Spinners: " + osuFormat.HitObjects.GetHitObjectList().Count(o => o.type.HasFlag(HitObjectType.Spinner)));

                Console.WriteLine("Time taken: " + formattedTime);
                Console.WriteLine("Completed: " + processedFiles + "/" + filePaths.Count + "\n");

            }
        }
        totalTime.Stop();
        TimeSpan totalTimeElapsed = totalTime.Elapsed;
        string formattedTotalTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
            totalTimeElapsed.Hours, totalTimeElapsed.Minutes, totalTimeElapsed.Seconds,
            totalTimeElapsed.Milliseconds);
        Console.WriteLine("Total time taken: " + formattedTotalTime);

    }
}