public class StatTrackerController : Controller
{
    private RoundStatsData _roundStatsData;

    void Start()
    {
        _roundStatsData = new RoundStatsData();
        actions.Add(GameEvents.ACTION_RIGHT_ANSWER, _roundStatsData.AddCorrentAnswer);
        actions.Add(GameEvents.ACTION_WRONG_ANSWER, _roundStatsData.AddWrongAnswer);
        actions.Add(GameEvents.RESTART_GAME, _roundStatsData.Reset);
    }
}
