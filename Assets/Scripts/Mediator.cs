using System.Collections.Generic;
using UnityEngine;

public class Mediator : MonoBehaviour
{
    [SerializeField] List<Controller> _controllers;

    void Start()
    {
        Notify(GameEvents.START_GAME);
    }

    public void Notify(string eventType)
    {
        foreach (var controller in _controllers)
            controller.Notify(eventType);
    }
}
