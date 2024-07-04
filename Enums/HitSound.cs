﻿namespace OsuFormatReader.Enums;

[Flags]
public enum HitSound : byte
{
    Normal = 1 << 0,
    Whistle = 1 << 1,
    Finish = 1 << 2,
    Clap = 1 << 3
}