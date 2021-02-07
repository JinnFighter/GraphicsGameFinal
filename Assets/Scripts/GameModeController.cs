using UnityEngine;

public abstract class GameModeController : MonoBehaviour
{
    public NewGameMode GameMode { get; protected set; }

    public abstract void Check(Pixel invoker);

    public delegate void GameModeEvent(string eventType);

    public event GameModeEvent GameEventGenerated;

    protected virtual void OnGameModeEvent(string eventType)
    {
        GameModeEvent handler = GameEventGenerated;
        handler?.Invoke(eventType);
    }
}
