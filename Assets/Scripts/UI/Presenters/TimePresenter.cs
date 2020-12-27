using System.Collections.Generic;
using UnityEngine;

public class TimePresenter : MonoBehaviour, ITimePresenter
{
    [SerializeField] private List<ITextView> _views;
    private TimerComponent _timer;
    private TimerFormat _timerFormat;

    // Start is called before the first frame update
    void Start()
    {
        _timer = GetComponent<TimerComponent>();
        _timerFormat = GetComponent<TimerFormat>();
    }

    public void OnTimeChange(float time) => _timerFormat.GetFormattedTime(time);
}
