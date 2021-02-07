public class StatTrackerController : Controller, IPausable
{
    private StopwatchComponent _stopwatch;
    private RoundStatsData _roundStatsData;

    void Start()
    {
        _stopwatch = GetComponent<StopwatchComponent>();
        _stopwatch.TimeChange += OnStopwatchTick;
        _roundStatsData = new RoundStatsData();
        actions.Add(GameEvents.ACTION_RIGHT_ANSWER, _roundStatsData.AddCorrentAnswer);
        actions.Add(GameEvents.ACTION_WRONG_ANSWER, _roundStatsData.AddWrongAnswer);
        actions.Add(GameEvents.RESTART_GAME, ResetData);
        actions.Add(GameEvents.START_GAME, OnStartGameEvent);
        actions.Add(GameEvents.GAME_OVER, OnGameOverEvent);
    }

    public RoundStatsData GetData() => _roundStatsData;

    public void ResetData()
    {
        _stopwatch.Stop();
        _stopwatch.ResetStopwatch();
        _roundStatsData.Reset();
    }

    public void Pause() => _stopwatch.Stop();

    public void Unpause() => _stopwatch.Resume();

    private void OnStopwatchTick(float time) => _roundStatsData.SetTimeSpent(time);

    void OnDestroy()
    {
        _stopwatch.TimeChange -= OnStopwatchTick;
    }

    private void OnGameOverEvent() => _stopwatch.Stop();

    private void OnStartGameEvent()
    {
        ResetData();
        _stopwatch.Resume();
    }
}
