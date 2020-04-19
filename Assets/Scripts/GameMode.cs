public abstract class GameMode
{
    protected readonly GameplayTimer timer;
    protected bool gameActive;
    protected bool gameStarted;
    protected int difficulty;

    public GameMode(GameplayTimer timer, int difficulty)
    {
        gameStarted = false;
        gameActive = false;
        timer = timer;
        difficulty = difficulty;
    }

    public abstract void CheckAction(Pixel invoker);

    public virtual void Pause()
    {
        if (gameStarted)
        {
            gameActive = false;
            timer.PauseTimer();
        }
    }

    public virtual void Continue()
    {
        if (gameStarted)
        {
            gameActive = true;
            timer.ResumeTimer();
        }
    }

    public abstract void ChangeGameState();

    public abstract void Restart();

    protected virtual bool CanCheckAction() => gameActive && timer.Counting;
    
}
