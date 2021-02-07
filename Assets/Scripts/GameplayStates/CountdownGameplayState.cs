using UnityEngine;

[RequireComponent(typeof(TimerComponent))]
public class CountdownGameplayState : GameplayState
{
    [SerializeField] private StatesContainer _statesContainer;
    [SerializeField] private TextView _timerText;
    private TimerComponent _timer;

    void Awake()
    {
        _timer = GetComponent<TimerComponent>();
    }

    public override void Init()
    {
        _timer.ResetTimer();
        _timer.StartTime = 4f;
        _timer.TimerEndEvent += NextState;
        _timer.Launch();
        _timerText.Activate();
    }

    public override void OnDelete()
    {
        _timer.StopTimer();
        _timerText.Deactivate();
        _timer.TimerEndEvent -= NextState;
    }

    private void NextState() => _statesContainer.NextState();

    protected override void OnPauseAction() => _timer.StopTimer();

    protected override void OnUnpauseAction() => _timer.ResumeTimer();
}
