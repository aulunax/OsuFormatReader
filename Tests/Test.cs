using System.Collections.Concurrent;
using System.Diagnostics;
using OsuFormatReader.DataTypes;
using OsuFormatReader.Enums;
using OsuFormatReader.IO;
using OsuFormatReader.Sections;

namespace OsuFormatReader.Tests;

public class Test
{
    private static List<string> GetOsuFiles(string folderPath)
    {
        var osuFiles = new List<string>();
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
        var filePaths = GetOsuFiles("D:\\osu full copy\\osu!\\Songs");
        var osuFormats = new ConcurrentBag<OsuFormat>();
        var timeSpans = new ConcurrentBag<TimeSpan>();

        var totalFiles = filePaths.Count;
        var processedFiles = 0;

        var totalTime = new Stopwatch();
        totalTime.Start();

        Parallel.ForEach(filePaths, filePath =>
        {
            using (var reader = new OsuFormatStreamReader(new FileStream(filePath, FileMode.Open)))
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var osuFormat = OsuFormat.Read(reader);

                stopwatch.Stop();
                var timeTaken = stopwatch.Elapsed;
                timeSpans.Add(timeTaken);
                var formattedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                    timeTaken.Hours, timeTaken.Minutes, timeTaken.Seconds,
                    timeTaken.Milliseconds);

                osuFormats.Add(osuFormat);
                // Console.WriteLine(osuFormat.Metadata.Artist + " - " + osuFormat.Metadata.Title + " [" +
                //                   osuFormat.Metadata.Version + "]");
                // Console.WriteLine("Beatmapset ID: " + osuFormat.Metadata.BeatmapSetID);
                // Console.WriteLine("Beatmap ID: " + osuFormat.Metadata.BeatmapID);
                // Console.WriteLine("Circles: " + osuFormat.HitObjects.GetHitObjectList().Count(o => o.type.HasFlag(HitObjectType.HitCircle)));
                // Console.WriteLine("Sliders: " + osuFormat.HitObjects.GetHitObjectList().Count(o => o.type.HasFlag(HitObjectType.Slider)));
                // Console.WriteLine("Spinners: " + osuFormat.HitObjects.GetHitObjectList().Count(o => o.type.HasFlag(HitObjectType.Spinner)));

                // Console.WriteLine("Time taken: " + formattedTime);

                var completed = Interlocked.Increment(ref processedFiles);
                // Console.WriteLine("Completed: " + completed + "/" + totalFiles + "\n");
            }
        });

        totalTime.Stop();
        var totalTimeElapsed = totalTime.Elapsed;
        var formattedTotalTime = string.Format("{0:00}:{1:00}:{2:00}.{3:000}",
            totalTimeElapsed.Hours, totalTimeElapsed.Minutes, totalTimeElapsed.Seconds,
            totalTimeElapsed.Milliseconds);
        Console.WriteLine("Total time taken: " + formattedTotalTime);
    }


    public static void Main()
    {
        var filePaths = GetOsuFiles("D:\\osu full copy\\osu!\\Songs");
        var osuFormats = new List<OsuFormat>();
        var timeSpans = new List<TimeSpan>();

        var processedFiles = 0;

        var totalTime = new Stopwatch();
        totalTime.Start();

        foreach (var filePath in filePaths)
        {
            processedFiles++;
            if (processedFiles > 10000)
                break;
            
            using (var reader = new OsuFormatStreamReader(new FileStream(filePath, FileMode.Open)))
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var osuFormat = OsuFormat.Read(reader);
                //var meta = Metadata.Read(reader);

                stopwatch.Stop();
                var timeTaken = stopwatch.Elapsed;
                timeSpans.Add(timeTaken);
                var formattedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                    timeTaken.Hours, timeTaken.Minutes, timeTaken.Seconds,
                    timeTaken.Milliseconds);

                osuFormats.Add(osuFormat);
                // Console.WriteLine(osuFormat.Metadata.Artist + " - " + osuFormat.Metadata.Title + " [" +
                //                   osuFormat.Metadata.Version + "]");
                // Console.WriteLine("Beatmapset ID: " + osuFormat.Metadata.BeatmapSetID);
                // Console.WriteLine("Beatmap ID: " + osuFormat.Metadata.BeatmapID);
                // Console.WriteLine("Circles: " + osuFormat.HitObjects.GetHitObjectList().Count(o => o.type.HasFlag(HitObjectType.HitCircle)));
                // Console.WriteLine("Sliders: " + osuFormat.HitObjects.GetHitObjectList().Count(o => o.type.HasFlag(HitObjectType.Slider)));
                // Console.WriteLine("Spinners: " + osuFormat.HitObjects.GetHitObjectList().Count(o => o.type.HasFlag(HitObjectType.Spinner)));

                //Console.WriteLine("Time taken: " + formattedTime);
                //Console.WriteLine("Completed: " + processedFiles + "/" + filePaths.Count);
            }
        }

        totalTime.Stop();
        var totalTimeElapsed = totalTime.Elapsed;
        var formattedTotalTime = string.Format("{0:00}:{1:00}:{2:00}.{3:000}",
            totalTimeElapsed.Hours, totalTimeElapsed.Minutes, totalTimeElapsed.Seconds,
            totalTimeElapsed.Milliseconds);
        Console.WriteLine("Total time taken: " + formattedTotalTime);
    }
}