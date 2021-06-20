using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Pixelgrid
{
    public class MainMenuDialog : MonoBehaviour, IMenuDialog
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private MenuMediator _menuMediator;
        [SerializeField] private GameObject _selectModePanel;
        [SerializeField] private SettingsDialog _settingsPanel;
        [SerializeField] private LoginDialog _loginPanel;
        [SerializeField] private Button _loginButton;

        private ProfilesManager _profilesManager;

        void Start()
        {
            _profilesManager = GetComponent<ProfilesManager>();
            _profilesManager.Load();
            _menuMediator.PushPanel(gameObject);
            if (_profilesManager.Container.profiles.Any())
                _loginButton.GetComponentInChildren<Text>().text = _profilesManager.ActiveProfile.name;
            else
                Notify("Login");
        }

        public void Notify(string eventType)
        {
           switch(eventType)
            {
                case "SelectMode":
                    _menuMediator.PushPanel(_selectModePanel);
                    break;
                case "Settings":
                    _menuMediator.PushPanel(_settingsPanel.GetPanel());
                    break;
                case "Login":
                    _menuMediator.PushPanel(_loginPanel.GetPanel());
                    _loginPanel.Notify("LoadProfiles");
                    break;
                case "LoginComplete":
                    _loginButton.GetComponentInChildren<Text>().text = _profilesManager.ActiveProfile.name;
                    break;
                case "Exit":
                    Application.Quit();
                    break;
                default:
                    break;
            }
        }

        public GameObject GetPanel() => _panel;
    }
}
