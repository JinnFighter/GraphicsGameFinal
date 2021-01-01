using System.Collections.Generic;

public class BrezenheimModeData
{
    public bool IsCorrectAnswer { get; private set; }
    public int Index { get; private set; }
    public IEnumerable<Position> Points { get; private set; }

    public BrezenheimModeData(bool isCorrectAnswer, int index, IEnumerable<Position> points)
    {
        IsCorrectAnswer = isCorrectAnswer;
        Index = index;
        Points = points;
    }
}
