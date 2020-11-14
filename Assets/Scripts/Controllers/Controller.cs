using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public abstract void Notify(string eventType);
}
