public class RoundStatsData
{
    private uint _correntAnswers;
    private uint _wrongAnswers;
    private float _timeSpent;

    public RoundStatsData()
    {
        _correntAnswers = 0;
        _wrongAnswers = 0;
        _timeSpent = 0;
    }

    public uint GetCorrectAnswersCount() => _correntAnswers;

    public uint GetWrongAnswersCount() => _wrongAnswers;

    public void AddCorrentAnswer() => _correntAnswers++;

    public void AddWrongAnswer() => _wrongAnswers++;

    public float GetTimeSpent() => _timeSpent;

    public void SetTimeSpent(float timeSpent) => _timeSpent = timeSpent;

    public void Reset()
    {
        _correntAnswers = 0;
        _wrongAnswers = 0;
        _timeSpent = 0;
    }
}
