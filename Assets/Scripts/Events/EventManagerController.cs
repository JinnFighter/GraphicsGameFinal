using UnityEngine;

public abstract class EventManagerController : MonoBehaviour
{
    public abstract void HandleEvent(string eventType);
}
