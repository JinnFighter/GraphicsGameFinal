using UnityEngine;

public abstract class EventHandler : MonoBehaviour, IEventHandler
{
    public abstract void Handle();
}
