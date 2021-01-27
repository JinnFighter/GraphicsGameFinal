using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int streak = 0;

    public delegate void ScoreChange(int currentScore);

    public event ScoreChange ScoreChangedEvent;

    public int Score { get; private set; } = 0;

    void Awake()
    {
        Messenger.AddListener(GameEvents.ACTION_RIGHT_ANSWER, AddScore);
        Messenger.AddListener(GameEvents.ACTION_WRONG_ANSWER, ResetStreak);
        Messenger.AddListener(GameEvents.RESTART_GAME, OnRestartGameEvent);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvents.ACTION_RIGHT_ANSWER, AddScore);
        Messenger.RemoveListener(GameEvents.ACTION_WRONG_ANSWER, ResetStreak);
        Messenger.RemoveListener(GameEvents.RESTART_GAME, OnRestartGameEvent);
    }

    public void AddScore()
    {
        if(streak < 5)
            streak++;
        Score += (int)(100 * (1 + 0.1 * streak));
        ScoreChangedEvent?.Invoke(Score);
    }

    public void ResetStreak() => streak = 0;

    public void OnRestartGameEvent()
    {
        Score = 0;
        streak = 0;
        ScoreChangedEvent?.Invoke(Score);
    }
}