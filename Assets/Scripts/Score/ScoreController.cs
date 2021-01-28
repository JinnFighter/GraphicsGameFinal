public class ScoreController : Controller
{
    private ScoreKeeper scoreKeeper;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreKeeper = GetComponent<ScoreKeeper>();
        actions.Add(GameEvents.ACTION_RIGHT_ANSWER, scoreKeeper.AddScore);
        actions.Add(GameEvents.ACTION_WRONG_ANSWER, scoreKeeper.ResetStreak);
        actions.Add(GameEvents.RESTART_GAME, scoreKeeper.OnRestartGameEvent);
    }
}
