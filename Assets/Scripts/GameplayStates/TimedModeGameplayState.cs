using UnityEngine;

public class TimedModeGameplayState : GameModeGameplayState
{
    [SerializeField] private StatesContainer _statesContainer;
    private GameModeController _gameModeController;
    private TimerComponent _timer;

    void Awake()
    {
        _timer = GetComponent<TimerComponent>();
        _gameModeController = GetComponent<GameModeController>();
    }

    public override void Init()
    {
        _gameModeController.GameEventGenerated += OnGameEvent;
        _gameModeController.GameMode.SetGameActive(true);
        float time;
        switch (_gameModeController.GameMode.GetDifficulty())
        {
            case 1:
                time = 80f;
                break;
            case 2:
                time = 120f;
                break;
            default:
                time = 60f;
                break;
        }
        _timer.StartTime = time;
        _timer.TimerEndEvent += NextState;
        _timer.Launch();
        Notify(GameEvents.START_GAME);
    }

    public override void OnDelete()
    {
        _timer.StopTimer();
        _timer.TimerEndEvent -= NextState;
        _gameModeController.GameMode.SetGameActive(false);
        _gameModeController.GameEventGenerated -= OnGameEvent;
    }

    private void OnGameEvent(string eventType)
    {
        if (eventType == GameEvents.GAME_OVER)
            _statesContainer.NextState();

        Notify(eventType);
    }

    public bool IsActive() => _gameModeController.GameMode.IsActive();

    private void NextState() => _statesContainer.NextState();

    protected override void OnPauseAction()
    {
        _timer.StopTimer();
        _gameModeController.GameMode.SetGameActive(false);
    }

    protected override void OnUnpauseAction()
    {
        _timer.ResumeTimer();
        _gameModeController.GameMode.SetGameActive(true);
    }

    public void OnRestart()
    {
        _gameModeController.GameMode.DoRestartAction();
        _timer.ResetTimer();
        Notify(GameEvents.RESTART_GAME);
    }
}
