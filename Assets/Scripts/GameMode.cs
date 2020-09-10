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
    }

    public abstract void CheckAction(Pixel invoker);

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

    public abstract void ChangeGameState();

    public abstract void Restart();

    protected bool CanCheckAction() => gameActive && eventReactor.CanCheckAction();
    
}
