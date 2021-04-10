using UnityEngine;
using UnityEngine.UI;

namespace Pixelgrid
{
    public class ProgressBar : MonoBehaviour
    {
        private Slider _slider;

        public float FillSpeed = 0.75f;

        private float _targetProgress = 0;

        void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        // Start is called before the first frame update
        void Start()
        {
            IncrementProgress(60);
        }

        // Update is called once per frame
        void Update()
        {
            if (_slider.value < _targetProgress)
                _slider.value += FillSpeed * Time.deltaTime;
        }

        public void IncrementProgress(int value) => _slider.value = value;
    }
}
