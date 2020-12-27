using System.Collections;
using UnityEngine;

public abstract class TextView : MonoBehaviour, ITextView, IActivatable
{
    public abstract void Activate();
    public abstract void Deactivate();
    public abstract bool IsActive();
    public abstract void SetText(string text);
}
