using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    private int streak = 0;
    [SerializeField] private Text scoreNumber;

    public int Score { get; set; } = 0;

    void Awake()
    {
        Messenger<int>.AddListener(GameEvents.ACTION_RIGHT_ANSWER, AddScore);
        Messenger.AddListener(GameEvents.ACTION_WRONG_ANSWER, ResetStreak);
        Messenger.AddListener(GameEvents.RESTART_GAME, OnRestartGameEvent);
    }

    void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEvents.ACTION_RIGHT_ANSWER, AddScore);
        Messenger.RemoveListener(GameEvents.ACTION_WRONG_ANSWER, ResetStreak);
        Messenger.RemoveListener(GameEvents.RESTART_GAME, OnRestartGameEvent);
    }

    public void AddScore(int points)
    {
        if(streak < 5)
            streak++;
        Score += (int)(points * (1 + 0.1 * streak));
        scoreNumber.text = Score.ToString();
    }

    public void ResetStreak() => streak = 0;

    public void OnRestartGameEvent()
    {
        Score = 0;
        streak = 0;
        scoreNumber.text = Score.ToString();
    }
}