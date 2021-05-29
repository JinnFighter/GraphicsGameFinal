using UnityEngine;
using UnityEngine.UI;

namespace Pixelgrid
{
    public class ProgressBar : MonoBehaviour
    {
        private Slider _slider;

        public Color Color { get => _slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color; set => _slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = value; }

        public float CurrentValue { get => _slider.value; }

        public float MaxValue { get => _slider.maxValue; set => _slider.maxValue = value; }

        void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        public void SetProgress(int value)
        {
            _slider.value = value;
        }

        public void IncrementProgress() => _slider.value++;
    }
}
