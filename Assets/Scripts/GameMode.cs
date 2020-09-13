public abstract class GameMode
{
    protected bool gameActive;
    protected bool gameStarted;
    protected int difficulty;
    protected IEventReactor eventReactor;

    public GameMode(int inputDifficulty)
    {
        gameStarted = false;
        gameActive = false;
        difficulty = inputDifficulty;
        Messenger<Pixel>.AddListener(GameEvents.GAME_CHECK, CheckAction);
        Messenger.AddListener(GameEvents.TIMER_STOP, ChangeGameState);
        Messenger.AddListener(GameEvents.PAUSE_GAME, Pause);
        Messenger.AddListener(GameEvents.CONTINUE_GAME, Continue);
        Messenger.AddListener(GameEvents.RESTART_GAME, Restart);
    }

    public void CheckAction(Pixel invoker)
    {
        if (!CanCheckAction()) return;

        Check(invoker);
    }

    public abstract void Check(Pixel invoker);

    public void Pause()
    {
        if (gameStarted)
        {
            gameActive = false;
            eventReactor.OnPause();
        }
    }

    public void Continue()
    {
        if (gameStarted)
        {
            gameActive = true;
            eventReactor.OnContinue();
        }
    }

    public void ChangeGameState()
    {
        if (gameStarted)
            gameActive = false;
        else
        {
            gameActive = true;
            gameStarted = true;
            eventReactor.OnChangeState(difficulty);
        }
    }

    public void Restart()
    {
        gameActive = false;
        gameStarted = false;

        DoRestartAction();

        eventReactor.OnRestart();

        Messenger.Broadcast(GameEvents.START_GAME);
    }

    public abstract void DoRestartAction();

    protected bool CanCheckAction() => gameActive && eventReactor.CanCheckAction();
}
