using System.Collections.Generic;

public class BrezenheimMode
{
    protected int curLine;
    protected List<LinesModeData> linesDatas;
    protected List<int>[] ds;

    public BrezenheimMode()
    {
        curLine = 0;
        linesDatas = new List<LinesModeData>();
    }

    public BrezenheimModeData GetData(Position position)
    {
        var isGameOver = false;
        var isCorrect = false;
        var d = 0;
        var points = new List<Position>();
        var canClearGrid = false;

        if (linesDatas[curLine].GetCurrentIndex() == linesDatas[curLine].GetPointsCount() - 1)
        {
            curLine++;
            if (curLine == linesDatas.Count)
                isGameOver = true;
            else
            {
                canClearGrid = true;
                isCorrect = true;
                points.Add(linesDatas[curLine].GetPoint(0));
                points.Add(linesDatas[curLine].GetPoint(linesDatas[curLine].GetPointsCount() - 1));
                d = ds[curLine][linesDatas[curLine].GetCurrentIndex()];
            }
        }
        else
        {
            if (position.Equals(linesDatas[curLine].GetCurrentPoint()))
            {
                isCorrect = true;
                points.Add(position);
                linesDatas[curLine].NextPoint();
                d = ds[curLine][linesDatas[curLine].GetCurrentIndex()];
            }
            else
                isCorrect = false;
        }

        return new BrezenheimModeData(isGameOver, isCorrect, d, points, canClearGrid);
    }
}
