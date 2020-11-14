using System.Collections.Generic;
using UnityEngine;

public class TimerGameController : Controller
{
    [SerializeField] private List<GameState> _gameStates;
    private int _currentState;

    void Awake()
    {
        _currentState = 0;
        _gameStates[_currentState].Init();
        Messenger.AddListener(GameEvents.TIMER_STOP, NextState);
        Messenger.AddListener(GameEvents.GAME_OVER, NextState);
    }

    void Start()
    {
        actions.Add(GameEvents.PAUSE_GAME, OnPauseEvent);
        actions.Add(GameEvents.CONTINUE_GAME, OnContinueEvent);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvents.TIMER_STOP, NextState);
        Messenger.RemoveListener(GameEvents.GAME_OVER, NextState);
    }

    public void NextState()
    {
        _gameStates[_currentState].OnDelete();
        _currentState++;
        if (_currentState >= _gameStates.Count)
            _currentState = 0;
        _gameStates[_currentState].Init();
    }

    private void OnPauseEvent()
    {
        if (_gameStates[_currentState] is IActivatable activatable)
            activatable.Deactivate();
    }

    private void OnContinueEvent()
    {
        if (_gameStates[_currentState] is IActivatable activatable)
            activatable.Activate();
    }
}
