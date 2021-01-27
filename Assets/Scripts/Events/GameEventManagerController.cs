using System.Collections.Generic;
using UnityEngine;

public class GameEventManagerController : EventManagerController
{
    [SerializeField] private List<Controller> _controllers;

    public override void HandleEvent(string eventType)
    {
        foreach (var controller in _controllers)
            controller.Notify(eventType);
    }
}
