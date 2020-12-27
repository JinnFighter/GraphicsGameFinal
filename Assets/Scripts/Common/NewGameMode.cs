public abstract class NewGameMode
{
    protected int difficulty;
    private bool _isGameActive;

    public NewGameMode(int inputDifficulty)
    {
        difficulty = inputDifficulty;
        _isGameActive = false;
    }

    public abstract void Check(Pixel invoker);
    public abstract void DoRestartAction();
    public void SetGameActive(bool value) => _isGameActive = value;
    public bool IsActive() => _isGameActive;
    public int GetDifficulty() => difficulty;
}
