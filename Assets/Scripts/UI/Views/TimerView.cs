using System;
using UnityEngine;
using UnityEngine.UI;

namespace Pixelgrid.UI.Views
{
    [RequireComponent(typeof(Text))]
    [RequireComponent(typeof(TimerFormat))]
    public class TimerView : MonoBehaviour, ITimerView
    {
        private Text _text;
        private TimerFormat _timerFormat;

        private void Awake()
        {
            _text = GetComponent<Text>();
            _timerFormat = GetComponent<TimerFormat>();
        }

        public void UpdateTime(float currentTime) => _text.text = _timerFormat.GetFormattedTime(currentTime);
    }
}
