using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private List<EventHandler> _eventHandlers;

    public void Notify()
    {
        foreach (var handler in _eventHandlers)
            handler.Handle();
    }
}
