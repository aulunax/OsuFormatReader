﻿namespace OsuFormatReader.Sections;
public class Difficulty
{
    public decimal HPDrainRate { get; set; }
    public decimal CircleSize { get; set; }
    public decimal OverallDifficulty { get; set; }
    public decimal ApproachRate { get; set; }
    public decimal SliderMultiplier { get; set; }
    public decimal SliderTickRate { get; set; }
    
    public static void Read(OsuFormatReader reader, Difficulty outobj)
    {
       
    }
}