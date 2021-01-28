public class RoundStatsData
{
    private uint _correntAnswers;
    private uint _wrongAnswers;

    public RoundStatsData()
    {
        _correntAnswers = 0;
        _wrongAnswers = 0;
    }

    public uint GetCorrectAnswersCount() => _correntAnswers;

    public uint GetWrongAnswersCount() => _wrongAnswers;

    public void AddCorrentAnswer() => _correntAnswers++;

    public void AddWrongAnswer() => _wrongAnswers++;

    public void Reset()
    {
        _correntAnswers = 0;
        _wrongAnswers = 0;
    }
}
