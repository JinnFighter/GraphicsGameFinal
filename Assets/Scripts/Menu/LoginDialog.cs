using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Pixelgrid
{
    public class LoginDialog : MonoBehaviour, IMenuDialog
    {
        [SerializeField] private ProfilesManager _profilesManager;
        [SerializeField] private GameObject _buttonTemplate;
        [SerializeField] private GameObject _loginPanel;
        [SerializeField] private Text _noProfilesText;
        [SerializeField] private MainMenuDialog _mainMenuDialog;

        private List<GameObject> profileButtons;

        void Start()
        {
            profileButtons = new List<GameObject>();
        }

        public void Notify(string eventType)
        {
            switch (eventType)
            {
                case "LoadProfiles":
                    if (_profilesManager.Container.profiles.Count > 0)
                    {
                        foreach (var profile in _profilesManager.Container.profiles)
                        {
                            var btn = Instantiate(_buttonTemplate) as GameObject;
                            btn.GetComponentInChildren<Text>().text = profile.name;
                            btn.transform.SetParent(_loginPanel.transform);
                            var buttonComponent = btn.GetComponent<Button>();
                            buttonComponent.onClick = new Button.ButtonClickedEvent();
                            buttonComponent.onClick.AddListener(
                             () => { SetActiveProfile(profile.name); }
                            );
                            btn.transform.localScale = _buttonTemplate.transform.localScale;
                            profileButtons.Add(btn);
                        }
                    }
                    else
                        _noProfilesText.gameObject.SetActive(true);
                    break;
                case "UnloadProfiles":
                    foreach (var obj in profileButtons)
                        Destroy(obj);

                    profileButtons.Clear();
                    break;
                default:
                    break;
            }
        }


        public void SetActiveProfile(string profileName)
        {
            var container = _profilesManager.Container;
            var profile = container.profiles.First(profile => profile.name == profileName);

            if (_profilesManager.ActiveProfile != null)
                _profilesManager.ActiveProfile.active = false;
            else
                foreach (var prof in _profilesManager.Container.profiles.Where(profile => profile.active))
                    prof.active = false;


            _profilesManager.Container.Save(ProfilesManager.path);
            Notify("UnloadProfiles");
            _mainMenuDialog.Notify("LoginComplete");
        }
    }
}
