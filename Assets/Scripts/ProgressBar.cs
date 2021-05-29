using UnityEngine;
using UnityEngine.UI;

namespace Pixelgrid
{
    public class ProgressBar : MonoBehaviour
    {
        private Slider _slider;

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
