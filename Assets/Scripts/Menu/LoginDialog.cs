using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Pixelgrid
{
    public class LoginDialog : MonoBehaviour, IMenuDialog
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private ProfilesManager _profilesManager;
        [SerializeField] private GameObject _buttonTemplate;
        [SerializeField] private GameObject _loginPanel;
        [SerializeField] private Text _noProfilesText;
        [SerializeField] private MenuMediator _menuMediator;
        [SerializeField] private MainMenuDialog _mainMenuDialog;

        private List<GameObject> _profileButtons;

        public GameObject GetPanel() => _panel;

        public void Notify(string eventType)
        {
            switch (eventType)
            {
                case "LoadProfiles":
                    if (_profileButtons == null)
                        _profileButtons = new List<GameObject>();
                    if (_profilesManager.Container.profiles.Count > 0)
                    {
                        foreach (var profile in _profilesManager.Container.profiles)
                        {
                            var btn = Instantiate(_buttonTemplate) as GameObject;
                            var textComponent = btn.GetComponentInChildren<Text>();
                            textComponent.text = profile.name;
                            btn.transform.SetParent(_loginPanel.transform);
                            var buttonComponent = btn.GetComponent<Button>();
                            buttonComponent.onClick.AddListener(
                             () => { this.SetActiveProfile(textComponent.text); }
                            );
                            btn.transform.localScale = _buttonTemplate.transform.localScale;
                            _profileButtons.Add(btn);
                            btn.SetActive(true);
                        }
                    }
                    else
                        _noProfilesText.gameObject.SetActive(true);
                    break;
                case "UnloadProfiles":
                    foreach (var obj in _profileButtons)
                        Destroy(obj);

                    _profileButtons.Clear();
                    break;
                default:
                    break;
            }
        }


        public void SetActiveProfile(string profileName)
        {
            var container = _profilesManager.Container;
            var profile = container.profiles.First(profile => profile.name == profileName);

            profile.active = true;
            _profilesManager.ActiveProfile = profile;

            _profilesManager.Save();

            while (_menuMediator.GetPanelCount() > 1)
                _menuMediator.PopPanel();
            Notify("UnloadProfiles");
            _mainMenuDialog.Notify("LoginComplete");
        }
    }
}
