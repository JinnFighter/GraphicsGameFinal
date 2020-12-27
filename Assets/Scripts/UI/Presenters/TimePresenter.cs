using System.Collections.Generic;
using UnityEngine;

public class TimePresenter : MonoBehaviour, ITimePresenter
{
    [SerializeField] private List<TextView> _views;
    private TimerComponent _timer;
    private TimerFormat _timerFormat;

    // Start is called before the first frame update
    void Start()
    {
        _timer = GetComponent<TimerComponent>();
        _timerFormat = GetComponent<TimerFormat>();
        _timer.TimeChange += OnTimeChange;
    }

    public void OnTimeChange(float time)
    {
        foreach(var view in _views)
            view.SetText(_timerFormat.GetFormattedTime(time));
    }

    void OnDestroy()
    {
        _timer.TimeChange -= OnTimeChange;
    }
}
