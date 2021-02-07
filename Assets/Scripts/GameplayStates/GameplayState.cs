using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class GameplayState : MonoBehaviour, IPausable
{
    [SerializeField] private List<Controller> _controllers;

    public abstract void OnDelete();
    public abstract void Init();


    protected abstract void OnPauseAction();

    protected abstract void OnUnpauseAction();

    public void Pause()
    {
        OnPauseAction();
        foreach (IPausable pausable in _controllers.Where(controller => controller is IPausable))
            pausable.Pause();
    }

    public void Unpause()
    {
        OnUnpauseAction();
        foreach (IPausable pausable in _controllers.Where(controller => controller is IPausable))
            pausable.Unpause();
    }

    public void Notify(string eventType)
    {
        foreach (var controller in _controllers)
            controller.Notify(eventType);
    }
}
