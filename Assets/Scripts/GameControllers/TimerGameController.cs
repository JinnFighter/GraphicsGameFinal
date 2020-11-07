using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerGameController : MonoBehaviour
{
    [SerializeField] private List<GameState> _gameStates;
    private int _currentState;

    void Awake()
    {
        _currentState = 0;
        _gameStates[_currentState].Init();
        Messenger.AddListener(GameEvents.TIMER_STOP, NextState);
    }

    public void NextState()
    {
        _gameStates[_currentState].OnDelete();
        _currentState++;
        if (_currentState >= _gameStates.Count)
            _currentState = 0;
        _gameStates[_currentState].Init();
    }
}
