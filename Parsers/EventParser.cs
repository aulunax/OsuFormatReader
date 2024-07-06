﻿using OsuFormatReader.Enums;
using OsuFormatReader.Interfaces;
using OsuFormatReader.Sections.EventTypes;
using OsuFormatReader.Sections.EventTypes.EventParamsTypes;

namespace OsuFormatReader.Parsers;

public class EventParser
{
    public static IEvent? ParseEvent(string value)
    {
        IEvent? returnEvent;
        List<string> eventInfo = ValueParser.ParseDelimitedStrings(value, 3);
        EventType type;
        if (int.TryParse(eventInfo[0], out int intType))
        {
            type = EventTypeExtensions.IntToEventType(intType);
        }
        else
        {
            type = EventTypeExtensions.StringToEventType(eventInfo[0]);
        }

        switch (type)
        {
            case EventType.Unknown:
                returnEvent = null;
                break;
            case EventType.Background:
                returnEvent = new BackgroundsEvent(
                    ParseBackgroundsEventParams(eventInfo[2])
                    );
                break;
            case EventType.Video:
                returnEvent = new VideosEvent(
                    int.Parse(eventInfo[1]),
                    ParseVideosEventParams(eventInfo[2])
                );
                break;
            case EventType.Break:
                returnEvent = new BreaksEvent(
                    int.Parse(eventInfo[1]),
                    ParseBreaksEventParams(eventInfo[2])
                );
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        
        return returnEvent;
    }

    public static BackgroundsEventParams ParseBackgroundsEventParams(string value)
    {
        List<string> eventParams = ValueParser.ParseDelimitedStrings(value,3);
        
        if (int.TryParse(eventParams[1], out int xOffset) && int.TryParse(eventParams[2], out int yOffset))
        {
            return new BackgroundsEventParams(eventParams[0], xOffset, yOffset);
        }
        throw new FormatException("BackgroundsEventParams could not be parsed as [string, int, int]");
    }
    
    public static VideosEventParams ParseVideosEventParams(string value)
    {
        List<string> eventParams = ValueParser.ParseDelimitedStrings(value,3);
        
        if (int.TryParse(eventParams[1], out int xOffset) && int.TryParse(eventParams[2], out int yOffset))
        {
            return new VideosEventParams(eventParams[0], xOffset, yOffset);
        }
        throw new FormatException("VideosEventParams could not be parsed as [string, int, int]");
    }
    
    public static BreaksEventParams ParseBreaksEventParams(string value)
    {
        if (int.TryParse(value, out int endTime))
        {
            return new BreaksEventParams(endTime);
        }
        throw new FormatException("BreaksEventParams could not be parsed as [int]");
    }
}