using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    public abstract void Init();
    public abstract void OnDelete();
}
