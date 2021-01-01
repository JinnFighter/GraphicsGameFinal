using System.Collections.Generic;

public class BrezenheimModeData
{
    public bool IsGameOver { get; private set; }
    public bool IsCorrectAnswer { get; private set; }
    public int Index { get; private set; }
    public IEnumerable<Position> Points { get; private set; }
    public bool CanClearGrid { get; private set; }

    public BrezenheimModeData(bool isGameOver, bool isCorrectAnswer, int index, IEnumerable<Position> points, bool canClearGrid)
    {
        IsGameOver = isGameOver;
        IsCorrectAnswer = isCorrectAnswer;
        Index = index;
        Points = points;
        CanClearGrid = canClearGrid;
    }
}
