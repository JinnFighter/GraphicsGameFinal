using NUnit.Framework;

public class RoundStatsDataTest
{
    private RoundStatsDataFactory _roundStatsDataFactory = new RoundStatsDataFactory();

    [Test]
    public void AddCorrectAnswer()
    {
        var statsData = _roundStatsDataFactory.GetObject();
        var correctAnswerCount = statsData.GetCorrectAnswersCount() + 1;

        statsData.AddCorrentAnswer();

        Assert.AreEqual(correctAnswerCount, statsData.GetCorrectAnswersCount());
    }

    [Test]
    public void AddWrongAnswer()
    {
        var statsData = _roundStatsDataFactory.GetObject();
        var correctAnswerCount = statsData.GetWrongAnswersCount() + 1;

        statsData.AddWrongAnswer();

        Assert.AreEqual(correctAnswerCount, statsData.GetWrongAnswersCount());
    }

    [Test]
    public void Reset()
    {
        var statsData = _roundStatsDataFactory.GetObject();
        statsData.AddCorrentAnswer();
        statsData.AddWrongAnswer();

        statsData.Reset();

        Assert.AreEqual(0, statsData.GetCorrectAnswersCount());
        Assert.AreEqual(0, statsData.GetWrongAnswersCount());
    }
}
