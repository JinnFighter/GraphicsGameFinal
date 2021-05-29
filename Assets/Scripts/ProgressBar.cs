using UnityEngine;
using UnityEngine.UI;

namespace Pixelgrid
{
    public class ProgressBar : MonoBehaviour
    {
        private Slider _slider;

        private Image _sliderFiller;

        public Color Color { get => _sliderFiller.color; set => _sliderFiller.color = value; }

        public float CurrentValue { get => _slider.value; set => _slider.value = value; }

        public float MaxValue { get => _slider.maxValue; set => _slider.maxValue = value; }

        void Awake()
        {
            _slider = GetComponent<Slider>();
            _sliderFiller = _slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>();
        }
    }
}
