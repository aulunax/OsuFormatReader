﻿namespace OsuFormatReader.Enums;

public class Colour
{
    public Colour(int red, int green, int blue)
    {
        Red = red;
        Green = green;
        Blue = blue;
    }

    public int Red { get; set; }
    public int Green { get; set; }
    public int Blue { get; set; }
}