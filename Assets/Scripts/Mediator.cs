using System.Collections.Generic;
using UnityEngine;

public class Mediator : MonoBehaviour
{
    [SerializeField] List<Controller> _controllers;

    void Awake()
    {
        //Messenger.AddListener(GameEvents.GAME_OVER, OnGameOver);
    }

    void Start()
    {
        //Notify(GameEvents.START_GAME);
    }

    void OnDestroy()
    {
        //Messenger.RemoveListener(GameEvents.GAME_OVER, OnGameOver);
    }

    public void Notify(string eventType)
    {
        foreach (var controller in _controllers)
            controller.Notify(eventType);
    }

    private void OnGameOver() => Notify(GameEvents.GAME_OVER);
}
