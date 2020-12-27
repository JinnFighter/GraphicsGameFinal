using System.Collections.Generic;
using UnityEngine;

public class Mediator : MonoBehaviour
{
    [SerializeField] List<Controller> _controllers;

    void Awake()
    {
        Messenger<int>.AddListener(GameEvents.ACTION_RIGHT_ANSWER, OnCorrectAnswer);
        Messenger.AddListener(GameEvents.ACTION_WRONG_ANSWER, OnWrongAnswer);
        Messenger.AddListener(GameEvents.GAME_OVER, OnGameOver);
    }

    void Start()
    {
        Notify(GameEvents.START_GAME);
    }

    void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEvents.ACTION_RIGHT_ANSWER, OnCorrectAnswer);
        Messenger.RemoveListener(GameEvents.ACTION_WRONG_ANSWER, OnWrongAnswer);
        Messenger.RemoveListener(GameEvents.GAME_OVER, OnGameOver);
    }

    public void Notify(string eventType)
    {
        foreach (var controller in _controllers)
            controller.Notify(eventType);
    }

    private void OnCorrectAnswer(int _) => Notify(GameEvents.ACTION_RIGHT_ANSWER);

    private void OnWrongAnswer() => Notify(GameEvents.ACTION_WRONG_ANSWER);

    private void OnGameOver() => Notify(GameEvents.GAME_OVER);
}
