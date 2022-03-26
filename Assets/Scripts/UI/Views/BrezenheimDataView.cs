using UnityEngine;
using UnityEngine.UI;

namespace Pixelgrid.UI.Views
{
    public class BrezenheimDataView : MonoBehaviour
    {
        [SerializeField] private Text _text;

        public void SetText(string text) => _text.text = text;
    }
}
