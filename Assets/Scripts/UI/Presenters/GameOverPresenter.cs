using System;
using UnityEngine;

public class GameOverPresenter : MonoBehaviour
{
    [SerializeField] private TextView _correctAnswersView;
    [SerializeField] private TextView _wrongAnswersView;
    [SerializeField] private TextView _percentsView;

    public void UpdateData(RoundStatsData data)
    {
        var correctCount = data.GetCorrectAnswersCount();
        var wrongCount = data.GetWrongAnswersCount();
        _correctAnswersView.SetText(correctCount.ToString());
        _wrongAnswersView.SetText(wrongCount.ToString());
        float sum = correctCount + wrongCount;
        var ratio = wrongCount == 0 ? 0 : (int)(Math.Abs(correctCount / sum) * 100);
        _percentsView.SetText(ratio.ToString() + "%");
    }
}
