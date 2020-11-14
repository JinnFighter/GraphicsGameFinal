using UnityEngine;

public class UiController : Controller
{
    [SerializeField] private GameObject _settingsPopup;

    public override void Notify(string eventType)
    {
        if(eventType == GameEvents.PAUSE_GAME)
        {
            _settingsPopup.SetActive(true);
        }
        else if(eventType == GameEvents.CONTINUE_GAME)
        {
            _settingsPopup.SetActive(false);
        }
    }
}
