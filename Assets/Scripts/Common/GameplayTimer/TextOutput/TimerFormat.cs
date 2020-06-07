using UnityEngine;

public abstract class TimerFormat : MonoBehaviour
{
    protected string Format { get; set; }
    protected string Template { get; set; }

    public abstract string GetFormattedTime(float currentTime);
}
