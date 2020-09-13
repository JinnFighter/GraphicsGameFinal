using System.Collections.Generic;
using UnityEngine.UI;

public class BrezenheimGameMode : GameMode
{
    protected List<LinesModeData> linesDatas;
    protected ILineGenerator lineGenerator;
    protected List<int>[] ds;
    protected Position lastPoint;
    protected Position prevPoint;
    protected int curLine;
    protected GameField gameField;
    protected InputField textField;

    public BrezenheimGameMode(GameplayTimer timer, int difficulty, GameField inputField, InputField nextTextField) : base(difficulty)
    {
        textField = nextTextField;
        gameField = inputField;
        linesDatas = new List<LinesModeData>(); 

        GenerateLines();

        textField.text = ds[0][0].ToString();

        eventReactor = new DefaultReactor(timer, difficulty);

        Messenger.Broadcast(GameEvents.START_GAME);
    }

    public override void Check(Pixel invoker)
    {
        if (prevPoint == lastPoint)
        {
            curLine++;
            if (curLine == linesDatas.Count)
            {
                Messenger.Broadcast(GameEvents.GAME_OVER);
                eventReactor.OnGameOver();
                return;
            }
            else
            {
                gameField.ClearGrid();
                Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 100);
                gameField.grid[(int)linesDatas[curLine].GetPoint(0).X, (int)linesDatas[curLine].GetPoint(0).Y].setPixelState(true);
                lastPoint = linesDatas[curLine].GetPoint(linesDatas[curLine].GetPointsCount() - 1);
                gameField.grid[(int)lastPoint.X, (int)lastPoint.Y].setPixelState(true);
              
                prevPoint = null;
                textField.text = ds[curLine][linesDatas[curLine].GetCurrentIndex()].ToString();
            }
        }
        else
        {
            prevPoint = linesDatas[curLine].GetCurrentPoint();

            if (invoker.X == prevPoint.X && invoker.Y == prevPoint.Y)
            {
                Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 100);
                invoker.setPixelState(true);
                linesDatas[curLine].NextPoint();
                textField.text = ds[curLine][linesDatas[curLine].GetCurrentIndex()].ToString();
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

        foreach (var line in lines)
        {
            var linePoints = Algorithms.GetBrezenheimLineData(line, out var ds);
            var i = lines.IndexOf(line);
            this.ds[i] = new List<int>();
            linesDatas[i] = new LinesModeData();
            for (var j = 0; j < ds.Count; j++)
                this.ds[i].Add(ds[j]);
            for (var j = 0; j < linePoints.Count; j++)
                linesDatas[i].AddPoint(linePoints[j]);
        }
        lastPoint = linesDatas[0].GetPoint(linesDatas[0].GetPointsCount() - 1);
        prevPoint = null;
        gameField.grid[(int)lines[0].GetStart().X, (int)lines[0].GetStart().Y].setPixelState(true);
        gameField.grid[(int)lastPoint.X, (int)lastPoint.Y].setPixelState(true);  
    }

    ~BrezenheimGameMode()
    {
        Messenger<Pixel>.RemoveListener(GameEvents.GAME_CHECK, CheckAction);
        Messenger.RemoveListener(GameEvents.TIMER_STOP, ChangeGameState);
        Messenger.RemoveListener(GameEvents.PAUSE_GAME, Pause);
        Messenger.RemoveListener(GameEvents.CONTINUE_GAME, Continue);
        Messenger.RemoveListener(GameEvents.RESTART_GAME, Restart);
    }
}
