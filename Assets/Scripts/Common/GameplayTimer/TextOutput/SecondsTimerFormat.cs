using System;

public class SecondsTimerFormat : TimerFormat
{
    void Awake()
    {
        Format = "{0:0}";
        Template = "0";
    }

    public override string GetFormattedTime(float currentTime) => String.Format(Format, (int)(currentTime % 60));
}
