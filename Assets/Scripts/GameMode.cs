public abstract class GameMode
{
    private readonly GameplayTimer _timer;
    private bool _gameActive;
    private bool _gameStarted;
    private int _difficulty;

    public GameMode(GameplayTimer timer, int difficulty)
    {
        _gameStarted = false;
        _gameActive = false;
        _timer = timer;
        _difficulty = difficulty;
    }

    public abstract void CheckAction(GridPixelScript invoker);

    public virtual void Pause()
    {
        if (_gameStarted)
        {
            _gameActive = false;
            _timer.PauseTimer();
        }
    }

    public virtual void Continue()
    {
        if (_gameStarted)
        {
            _gameActive = true;
            _timer.ResumeTimer();
        }
    }

    public abstract void Restart();
}
