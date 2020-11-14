public abstract class GameMode
{
    private bool _isGameActive;
    private bool _hasGameStarted;
    protected int difficulty;
    protected IEventReactor eventReactor;

    public GameMode(int inputDifficulty)
    {
        _hasGameStarted = false;
        _isGameActive = false;
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
        if (_hasGameStarted)
        {
            _isGameActive = false;
            eventReactor.OnPause();
        }
    }

    public void Continue()
    {
        if (_hasGameStarted)
        {
            _isGameActive = true;
            eventReactor.OnContinue();
        }
    }

    public void ChangeGameState()
    {
        if (_hasGameStarted)
            _isGameActive = false;
        else
        {
            _isGameActive = true;
            _hasGameStarted = true;
            eventReactor.OnChangeState(difficulty);
        }
    }

    public void Restart()
    {
        _isGameActive = false;
        _hasGameStarted = false;

        DoRestartAction();

        eventReactor.OnRestart();

        Messenger.Broadcast(GameEvents.START_GAME);
    }

    public abstract void DoRestartAction();

    protected bool CanCheckAction() => _isGameActive && eventReactor.CanCheckAction();

    public void SetGameActive(bool value) => _isGameActive = value;
}
