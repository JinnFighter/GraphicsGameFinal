using UnityEngine;

public class UiController : Controller
{
    [SerializeField] private GameObject _settingsPopup;

    void Start()
    {
        actions.Add(GameEvents.PAUSE_GAME, OnPauseEvent);
        actions.Add(GameEvents.CONTINUE_GAME, OnContinueEvent);
    }

    private void OnPauseEvent()
    {
        _settingsPopup.SetActive(true);
    }

    private void OnContinueEvent()
    {
        _settingsPopup.SetActive(false);
    }
}
