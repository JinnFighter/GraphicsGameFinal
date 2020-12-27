using System.Collections.Generic;

public class NewBrezenheimGameMode : NewGameMode
{
    protected List<LinesModeData> linesDatas;
    protected ILineGenerator lineGenerator;
    protected List<int>[] ds;
    protected Position lastPoint;
    protected Position prevPoint;
    protected int curLine;
    protected GameFieldController gameField;

    public delegate void DChanged(int currentD);

    public event DChanged DChangedEvent;

    public NewBrezenheimGameMode(int difficulty, GameFieldController inputField) 
        : base(difficulty)
    {
        gameField = inputField;
        linesDatas = new List<LinesModeData>();
    }

    public override void Check(Pixel invoker)
    {
        if (prevPoint == lastPoint)
        {
            curLine++;
            if (curLine == linesDatas.Count)
            {
                Messenger.Broadcast(GameEvents.GAME_OVER);
                return;
            }
            else
            {
                gameField.ClearGrid();
                Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 100);
                gameField.SetState((int)linesDatas[curLine].GetPoint(0).X, (int)linesDatas[curLine].GetPoint(0).Y, true);
                lastPoint = linesDatas[curLine].GetPoint(linesDatas[curLine].GetPointsCount() - 1);
                gameField.SetState((int)lastPoint.X, (int)lastPoint.Y, true);

                prevPoint = null;
                DChangedEvent?.Invoke(ds[curLine][linesDatas[curLine].GetCurrentIndex()]);
            }
        }
        else
        {
            prevPoint = linesDatas[curLine].GetCurrentPoint();

            if (invoker.Position.Equals(prevPoint))
            {
                Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 100);
                invoker.SetState(true);
                linesDatas[curLine].NextPoint();
                DChangedEvent?.Invoke(ds[curLine][linesDatas[curLine].GetCurrentIndex()]);
                prevPoint = linesDatas[curLine].GetCurrentPoint();
            }
            else
                Messenger.Broadcast(GameEvents.ACTION_WRONG_ANSWER);
        }
    }

    public override void DoRestartAction()
    {
        gameField.ClearGrid();

        curLine = 0;

        GenerateLines();

        DChangedEvent?.Invoke(ds[0][0]);
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
        lineGenerator = new RandomLineGenerator(minLength, maxLength, maxLengthSum);
        var lines = lineGenerator.Generate(linesCount);

        ds = new List<int>[linesCount];
        linesDatas = new List<LinesModeData>(linesCount);

        for(var i = 0; i < lines.Count; i++)
        {
            var linePoints = Algorithms.GetBrezenheimLineData(lines[i], out var coeffs);
            ds[i] = new List<int>();
            linesDatas.Add(new LinesModeData());
            for (var j = 0; j < coeffs.Count; j++)
                ds[i].Add(coeffs[j]);
            for (var j = 0; j < linePoints.Count; j++)
                linesDatas[i].AddPoint(linePoints[j]);
        }

        lastPoint = linesDatas[0].GetPoint(linesDatas[0].GetPointsCount() - 1);
        prevPoint = null;
        var startPoint = lines[0].GetStart();
        gameField.SetState((int)startPoint.X, (int)startPoint.Y, true);
        gameField.SetState((int)lastPoint.X, (int)lastPoint.Y, true);
    }
}
