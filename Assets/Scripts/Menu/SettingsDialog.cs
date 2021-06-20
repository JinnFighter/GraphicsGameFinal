using UnityEngine;
using UnityEngine.UI;

namespace Pixelgrid
{
    public class SettingsDialog : MonoBehaviour, IMenuDialog
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Toggle _fullscreenToggle;

        void Start()
        {
            _fullscreenToggle.isOn = Screen.fullScreen;
        }

        public void Notify(string eventType)
        {
            switch(eventType)
            {
                case "Fullscreen":
                    var setFullScreen = !Screen.fullScreen;
                    _fullscreenToggle.isOn = setFullScreen;
                    Screen.fullScreen = setFullScreen;
                    break;
                default:
                    break;
            }
        }

        public GameObject GetPanel() => _panel;
    }
}
