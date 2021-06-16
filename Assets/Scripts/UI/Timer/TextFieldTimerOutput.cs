using UnityEngine;
using UnityEngine.UI;

public class TextFieldTimerOutput : TimerOutput
{
    [SerializeField] private TimerFormat _timerFormat;
    [SerializeField] private Text _timerText;

    public override void DisplayTime(float currentTime)
    {
        _timerText.text = _timerFormat.GetFormattedTime(currentTime);
    }
}
