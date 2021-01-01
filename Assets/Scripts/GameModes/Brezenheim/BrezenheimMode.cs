using System.Collections.Generic;

public class BrezenheimMode : GameMode
{
    protected int curLine;
    protected List<LinesModeData> linesDatas;
    protected List<int>[] ds;

    public BrezenheimMode(int difficulty) : base(difficulty) 
    {
        curLine = 0;
        linesDatas = new List<LinesModeData>();
    }

    public BrezenheimModeData GetData(Position position)
    {
        var isGameOver = false;
        var isCorrect = false;
        var d = ds[curLine][linesDatas[curLine].GetCurrentIndex()];
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

    protected virtual void GenerateLines()
    {
        int minLength;
        int maxLength;
        int maxLengthSum;
        int linesCount;
        switch (difficulty)
        {
            case 1:
                linesCount = 7;
                minLength = 4;
                maxLength = 8;
                maxLengthSum = 48;
                break;
            case 2:
                linesCount = 10;
                minLength = 5;
                maxLength = 10;
                maxLengthSum = 90;
                break;
            default:
                linesCount = 5;
                minLength = 2;
                maxLength = 5;
                maxLengthSum = 20;
                break;
        }
        var lineGenerator = new RandomLineGenerator(minLength, maxLength, maxLengthSum);
        var lines = lineGenerator.Generate(linesCount);

        ds = new List<int>[linesCount];
        linesDatas = new List<LinesModeData>(linesCount);

        for (var i = 0; i < lines.Count; i++)
        {
            var linePoints = Algorithms.GetBrezenheimLineData(lines[i], out var coeffs);
            ds[i] = new List<int>();
            linesDatas.Add(new LinesModeData());
            ds[i].AddRange(coeffs);
            linesDatas[i].AddRange(linePoints);
        }
    }

    public int GetDIndex() => ds[curLine][linesDatas[curLine].GetCurrentIndex()];

    public override void Check(Pixel invoker)
    {
        
    }

    public override void DoRestartAction() => GenerateLines();
}
