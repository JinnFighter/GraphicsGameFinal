using UnityEngine;

namespace Pixelgrid
{
    public class MainMenuDialog : MonoBehaviour, IMenuDialog
    {
        [SerializeField] private MenuMediator _menuMediator;
        [SerializeField] private GameObject _selectModePanel;
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private GameObject _loginPanel;

        public void Notify(string eventType)
        {
           switch(eventType)
            {
                case "SelectMode":
                    _menuMediator.PushPanel(_selectModePanel);
                    break;
                case "Settings":
                    _menuMediator.PushPanel(_settingsPanel);
                    break;
                case "Login":
                    _menuMediator.PushPanel(_loginPanel);
                    break;
                case "Exit":
                    Application.Quit();
                    break;
                default:
                    break;
            }
        }
    }
}
