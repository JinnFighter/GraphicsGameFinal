using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedModeGameplayState : GameModeGameplayState
{
    private GameModeController _gameModeController;
    private TimerComponent _timer;

    void Awake()
    {
        _timer = GetComponent<TimerComponent>();
        _gameModeController = GetComponent<GameModeController>();
    }

    // Update is called once per frame
    void Update()
    {

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
}
