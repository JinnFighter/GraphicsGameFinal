using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int streak = 0;

    public delegate void ScoreChange(int currentScore);

    public event ScoreChange ScoreChangedEvent;

    public int Score { get; private set; } = 0;

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