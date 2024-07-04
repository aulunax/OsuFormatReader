﻿using OsuFormatReader.DataTypes;
using OsuFormatReader.Enums;
using OsuFormatReader.Interfaces;
using OsuFormatReader.Sections.HitObjectTypes.ObjectParamsTypes;

namespace OsuFormatReader.Sections.HitObjectTypes;

public class Spinner : IHitObject<SpinnerParams>
{
    public int x { get; set; }
    public int y { get; set; }
    public int time { get; set; }
    public HitObjectType type { get; set; }
    public HitSound hitSound { get; set; }
    public SpinnerParams objectParams { get; set; }
    public HitSample hitSample { get; set; }

    public Spinner(int x, int y, int time, HitObjectType type, HitSound hitSound, SpinnerParams objectParams, HitSample hitSample)
    {
        this.x = x;
        this.y = y;
        this.time = time;
        this.type = type;
        this.hitSound = hitSound;
        this.objectParams = objectParams;
        this.hitSample = hitSample;
    }
}