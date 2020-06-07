using System;

public class MinutesTimerFormat : TimerFormat
{
    void Awake()
    {
        Format = "{0:00}:{1:00}:{2:000}";
        Template = "00:00:000";
    }

    public override string GetFormattedTime(float currentTime) => String.Format(Format, (int)(currentTime / 60f) % 60,
                    (int)(currentTime % 60), (int)(currentTime * 1000f) % 1000);
}
