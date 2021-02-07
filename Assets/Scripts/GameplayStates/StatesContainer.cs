using System.Collections.Generic;
using UnityEngine;

public class StatesContainer : MonoBehaviour, IPausable
{
    [SerializeField] private List<GameplayState> _gameStates;
    private int _currentState;

    void Awake()
    {
        _currentState = 0;
    }

    public void NextState()
    {
        _gameStates[_currentState].OnDelete();
        _currentState++;
        if (_currentState >= _gameStates.Count)
            _currentState = 0;
        _gameStates[_currentState].Init();
    }

    public void OnStartEvent() => _gameStates[_currentState].Init();

    public void Pause() => _gameStates[_currentState].Pause();

    public void Unpause() => _gameStates[_currentState].Unpause();
}
