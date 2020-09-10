public interface IEventReactor
{
    void OnGameOver();
    void OnChangeState(int difficulty);
    void OnRestart();
    void OnPause();
    void OnContinue();
    bool CanCheckAction();
}
