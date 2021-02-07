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
        _gameModeController.GameEventGenerated += Notify;
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
    }

    public override void OnDelete()
    {
        _timer.StopTimer();
        _timer.TimerEndEvent -= NextState;
        _gameModeController.GameMode.SetGameActive(false);
        _gameModeController.GameEventGenerated -= Notify;
    }

    public bool IsActive() => _gameModeController.GameMode.IsActive();

    private void NextState() => _statesContainer.NextState();
}
