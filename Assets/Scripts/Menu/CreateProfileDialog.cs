using UnityEngine;
using UnityEngine.UI;

namespace Pixelgrid
{
    public class CreateProfileDialog : MonoBehaviour, IMenuDialog
    {
        [SerializeField] private InputField _textField;
        [SerializeField] private MenuControls _menuControls;
        
        public void Notify(string eventType)
        {
            switch(eventType)
            {
                case "CreateProfile":
                    _menuControls.SubmitNewProfile(_textField);
                    break;
                default:
                    break;
            }
        }
    }
}
