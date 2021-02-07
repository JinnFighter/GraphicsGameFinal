using System.Collections.Generic;
using UnityEngine;

public class TimerGameController : Controller
{
    [SerializeField] private List<GameState> _gameStates;
    private int _currentState;

    void Awake()
    {
        _currentState = 0;
        //Messenger.AddListener(GameEvents.TIMER_STOP, NextState);
        actions.Add(GameEvents.PAUSE_GAME, OnPauseEvent);
        actions.Add(GameEvents.CONTINUE_GAME, OnContinueEvent);
        actions.Add(GameEvents.START_GAME, OnStartEvent);
        actions.Add(GameEvents.GAME_OVER, NextState);
    }

    void OnDestroy()
    {
        //Messenger.RemoveListener(GameEvents.TIMER_STOP, NextState);
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

    private void OnStartEvent() => _gameStates[_currentState].Init();
}
