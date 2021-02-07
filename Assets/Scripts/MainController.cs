using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour, IPausable
{
    [SerializeField] private StatesContainer _statesContainer;
    [SerializeField] List<Controller> _controllers;

    // Start is called before the first frame update
    void Start()
    {
        _statesContainer.OnStartEvent();
    }

    public void Pause()
    {
        _statesContainer.Pause();
        Notify(GameEvents.PAUSE_GAME);
    }

    public void Unpause()
    {
        _statesContainer.Unpause();
        Notify(GameEvents.CONTINUE_GAME);
    }

    private void Notify(string eventType)
    {
        foreach (var controller in _controllers)
            controller.Notify(eventType);
    }
}
