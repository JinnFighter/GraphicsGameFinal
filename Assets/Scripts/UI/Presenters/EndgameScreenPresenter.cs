using System;
using UnityEngine;

namespace Pixelgrid
{
    public class EndgameScreenPresenter : MonoBehaviour
    {
        [SerializeField] private TextView _timeElapsedView;
        [SerializeField] private TextView _correctAnswersView;
        [SerializeField] private TextView _wrongAnswersView;
        [SerializeField] private TextView _percentsView;

        public void UpdateData(StatsData data)
        {
            var correctCount = data.CorrectAnswers;
            var wrongCount = data.WrongAnswers;
            _correctAnswersView.SetText(correctCount.ToString());
            _wrongAnswersView.SetText(wrongCount.ToString());
            float sum = correctCount + wrongCount;
            var ratio = wrongCount == 0 ? correctCount == 0 ? 0 : 100 : (int)(Math.Abs(correctCount / sum) * 100);
            _percentsView.SetText(ratio.ToString() + "%");

            var timeSpent = data.TimeSpent;
            _timeElapsedView.SetText(GetComponent<TimerFormat>().GetFormattedTime(timeSpent));
        }
    }
}
