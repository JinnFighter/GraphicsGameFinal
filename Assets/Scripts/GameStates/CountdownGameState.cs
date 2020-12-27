using UnityEngine;

[RequireComponent(typeof(TimerComponent))]
public class CountdownGameState : GameState
{
    private TimerComponent _timer;
    [SerializeField] private TextView _timerText;

    void Awake()
    {
        _timer = GetComponent<TimerComponent>();
    }

    public override void Init()
    {
        _timer.ResetTimer();
        _timer.StartTime = 4f;
        _timer.Launch();
        _timerText.Activate();
    }

    public override void OnDelete()
    {
        _timer.StopTimer();
        _timerText.Deactivate();
    }
}
