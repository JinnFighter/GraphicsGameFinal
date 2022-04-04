using UnityEngine;
using UnityEngine.UI;

namespace Pixelgrid.UI.Views
{
    public class TurtlePathView : MonoBehaviour
    {
        [SerializeField] private Text _pathText;

        public void SetText(string text) => _pathText.text = text;
    }
}
