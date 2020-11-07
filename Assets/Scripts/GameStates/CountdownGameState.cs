public class CountdownGameState : GameState
{
    private TimerComponent _timer;

    void Awake()
    {
        _timer = GetComponent<TimerComponent>();
        _timer.StartTime = 4f;
    }

    public override void Init()
    {
        _timer.Launch();
        _timer.GetOutput().gameObject.SetActive(true);
    }

    public override void OnDelete()
    {
        _timer.StopTimer();
        _timer.GetOutput().gameObject.SetActive(false);
    }
}
