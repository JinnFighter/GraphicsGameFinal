using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Pixelgrid
{
    public class CreateProfileDialog : MonoBehaviour, IMenuDialog
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private InputField _textField;
        [SerializeField] private ProfilesManager _profilesManager;
        [SerializeField] private MenuMediator _menuMediator;

        public GameObject GetPanel() => _panel;

        public void Notify(string eventType)
        {
            switch (eventType)
            {
                case "CreateProfile":
                    var playerName = _textField.text;

                    if (!IsCorrectNickname(playerName))
                        return;

                    var profile = new PlayerProfile { name = playerName, active = true };
                    _profilesManager.Container.profiles.Add(profile);
                    
                    _profilesManager.ActiveProfile = profile;
                    _profilesManager.Save();

                    PlayerPrefs.SetString("PlayerName", playerName);
                    
                    while(_menuMediator.GetPanelCount() > 1)
                        _menuMediator.PopPanel();   
                    break;
                default:
                    break;
            }
        }

        private bool IsCorrectNickname(string playerName) => playerName != "" && !_profilesManager.Container.profiles.Any(profile => profile.name == playerName);
    }
}
