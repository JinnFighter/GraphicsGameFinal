using System.Collections.Generic;
using UnityEngine;

public class Mediator : MonoBehaviour
{
    [SerializeField] List<Controller> _controllers;

    void Awake()
    {
        Messenger.AddListener(GameEvents.PAUSE_GAME, Pause);
        Messenger.AddListener(GameEvents.CONTINUE_GAME, Continue);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvents.PAUSE_GAME, Pause);
        Messenger.RemoveListener(GameEvents.CONTINUE_GAME, Continue);
    }

    public void Pause()
    {
        foreach (var controller in _controllers)
            controller.Notify(GameEvents.PAUSE_GAME);
    }

    public void Continue()
    {
        foreach (var controller in _controllers)
            controller.Notify(GameEvents.CONTINUE_GAME);
    }
}
