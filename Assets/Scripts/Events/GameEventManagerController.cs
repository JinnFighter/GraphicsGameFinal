public class GameEventManagerController : EventManagerController
{
    public delegate void GameEvent();

    public event GameEvent CorrectAnswerEvent;

    public event GameEvent WrongAnswerEvent;

    public event GameEvent RestartGameEvent;

    public override void HandleEvent(string eventType)
    {
        switch(eventType)
        {
            case GameEvents.ACTION_RIGHT_ANSWER:
                CorrectAnswerEvent?.Invoke();
                break;
            case GameEvents.ACTION_WRONG_ANSWER:
                WrongAnswerEvent?.Invoke();
                break;
            case GameEvents.RESTART_GAME:
                RestartGameEvent?.Invoke();
                break;
        }
    }
}
