public class DefaultReactor : IEventReactor
{
    private GameplayTimer _timer;

    public DefaultReactor(GameplayTimer timer, int difficulty)
    {
        _timer = timer;
        SetupTimer(difficulty);
    }

    public bool CanCheckAction() => _timer.Counting;

    public void OnChangeState(int difficulty)
    {
        SetupTimer(difficulty);

        _timer.StartTimer();
    }

    public void OnContinue() => _timer.ResumeTimer();

    public void OnGameOver() => _timer.StopTimer();

    public void OnPause() => _timer.PauseTimer();

    public void OnRestart() => _timer.ResetTimer();

    private void SetupTimer(int difficulty)
    {
        switch (difficulty)
        {
            case 1:
                _timer.SetStartTime(80f);
                break;
            case 2:
                _timer.SetStartTime(120f);
                break;
            default:
                _timer.SetStartTime(60f);
                break;
        }
    }
}
