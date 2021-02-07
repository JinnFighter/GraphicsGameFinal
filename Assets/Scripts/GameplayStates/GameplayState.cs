using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class GameplayState : MonoBehaviour, IPausable
{
    [SerializeField] private List<Controller> _controllers;

    public abstract void OnDelete();
    public abstract void Init();

    public void Pause()
    {
        foreach (IPausable pausable in _controllers.Where(controller => controller is IPausable))
            pausable.Pause();
    }

    public void Unpause()
    {
        foreach (IPausable pausable in _controllers.Where(controller => controller is IPausable))
            pausable.Unpause();
    }
}
