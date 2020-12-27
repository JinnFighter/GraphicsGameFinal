using System.Collections.Generic;
using UnityEngine;

public class TextScorePresenter : MonoBehaviour, IScorePresenter
{
    [SerializeField] private List<TextView> _views;
    private ScoreKeeper _scoreKeeper;

    public void OnScoreChanged(int currentScore)
    {
        foreach(var view in _views)
            view.SetText(currentScore.ToString());
    }

    // Start is called before the first frame update
    void Start()
    {
        _scoreKeeper = GetComponent<ScoreKeeper>();
        _scoreKeeper.ScoreChangedEvent += OnScoreChanged;
    }

    void OnDestroy()
    {
        _scoreKeeper.ScoreChangedEvent -= OnScoreChanged;
    }
}
