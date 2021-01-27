using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private GameEventManagerController gameEventManagerController;
    private ScoreKeeper scoreKeeper;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreKeeper = GetComponent<ScoreKeeper>();
        gameEventManagerController.CorrectAnswerEvent += scoreKeeper.AddScore;
        gameEventManagerController.WrongAnswerEvent += scoreKeeper.ResetStreak;
        gameEventManagerController.RestartGameEvent += scoreKeeper.OnRestartGameEvent;
    }

    void OnDestroy()
    {
        gameEventManagerController.CorrectAnswerEvent -= scoreKeeper.AddScore;
        gameEventManagerController.WrongAnswerEvent -= scoreKeeper.ResetStreak;
        gameEventManagerController.RestartGameEvent -= scoreKeeper.OnRestartGameEvent;
    }
}
