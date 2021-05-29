using UnityEngine;
using UnityEngine.UI;

namespace Pixelgrid
{
    public class TutorialScreenPresenter : MonoBehaviour
    {
        [SerializeField] private Text _text;

        public void SetText(string text) => _text.text = text;
    }
}
