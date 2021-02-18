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

    public override string Check(Position invoker)
    {
        var actions = new List<IGameFieldAction>();
        var eventType = "";

        if (curLine >= linesDatas.Count)
            return GameEvents.GAME_OVER;

        var lineData = linesDatas[curLine];
        if (invoker.Equals(lineData.GetCurrentPoint()))
        {
            if (lineData.GetCurrentIndex() == lineData.GetPointsCount() - 1)
            {
                curLine++;
                if (curLine >= linesDatas.Count)
                    return GameEvents.GAME_OVER;
                else
                {
                    actions.Add(new ClearGameFieldAction());

                    actions.Add(new FillGameFieldAction(new List<Position>
                    {
                        linesDatas[curLine].GetPoint(0),
                        linesDatas[curLine].GetPoint(linesDatas[curLine].GetPointsCount() - 1)
                    }));
                }
            }
            else
            {
                actions.Add(new FillGameFieldAction(new List<Position> { invoker }));
                linesDatas[curLine].NextPoint();
            }

            DChangedEvent?.Invoke(ds[curLine][linesDatas[curLine].GetCurrentIndex()]);
            eventType = GameEvents.ACTION_RIGHT_ANSWER;
        }
        else
            eventType = GameEvents.ACTION_WRONG_ANSWER;

        foreach (var action in actions)
            action.DoAction(gameField);

        return eventType;
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
            ds[i].AddRange(coeffs);
            linesDatas[i].AddRange(linePoints);
        }

        lastPoint = linesDatas[0].GetPoint(linesDatas[0].GetPointsCount() - 1);
        prevPoint = null;
        var startPoint = lines[0].GetStart();

        var action = new FillGameFieldAction(new List<Position> { startPoint, lastPoint });
        action.DoAction(gameField);
    }

    public int GetLinesCount() => linesDatas.Count;

    public int GetCurrentLine() => curLine;
}
