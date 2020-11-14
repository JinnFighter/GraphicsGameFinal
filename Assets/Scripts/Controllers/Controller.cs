using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    protected Dictionary<string, Action> actions = new Dictionary<string, Action>();

    public void Notify(string eventType)
    {
        if (actions.TryGetValue(eventType, out var action))
            action();
    }
}
