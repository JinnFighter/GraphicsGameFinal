using UnityEngine;

[RequireComponent(typeof(TimerComponent))]
public class TimedGameplayState : GameState, IActivatable
{
    private GameModeController _gameModeController;
    private TimerComponent _timer;

    private void Awake()
    {
        _timer = GetComponent<TimerComponent>();
        _gameModeController = GetComponent<GameModeController>();
    }

    public override void Init()
    {
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
        _timer.Launch();
    }

    public override void OnDelete()
    {
        _timer.StopTimer();
        _gameModeController.GameMode.SetGameActive(false);
    }

    public bool IsActive() => _gameModeController.GameMode.IsActive();

    public void Activate()
    {
        _timer.ResumeTimer();
        _gameModeController.GameMode.SetGameActive(true);
    }

    public void Deactivate()
    {
        _timer.StopTimer();
        _gameModeController.GameMode.SetGameActive(false);
    }
}
