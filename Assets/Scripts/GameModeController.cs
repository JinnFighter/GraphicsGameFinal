using UnityEngine;

public abstract class GameModeController : MonoBehaviour
{
    public NewGameMode GameMode { get; protected set; }

    public abstract void Check(Pixel invoker);

    public delegate void GameEvent(string eventType);

    public event GameEvent GameEventGenerated;
}
