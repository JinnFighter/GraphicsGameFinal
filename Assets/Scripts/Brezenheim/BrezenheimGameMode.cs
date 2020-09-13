using System.Collections.Generic;
using UnityEngine.UI;

public class BrezenheimGameMode : GameMode
{
    protected ILineGenerator lineGenerator;
    protected List<Position>[] linePoints;
    protected List<int>[] ds;
    protected Position lastPoint;
    protected Position prevPoint;
    protected int iteration;
    protected int curLine;
    protected GameField gameField;
    protected InputField textField;

    public BrezenheimGameMode(GameplayTimer timer, int difficulty, GameField inputField, InputField nextTextField) : base(difficulty)
    {
        textField = nextTextField;
        gameField = inputField;

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
            if (curLine == linePoints.Length)
            {
                Messenger.Broadcast(GameEvents.GAME_OVER);
                eventReactor.OnGameOver();
                return;
            }
            else
            {
                iteration = 0;
                gameField.ClearGrid();
                Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 100);
                gameField.grid[(int)linePoints[curLine][0].X, (int)linePoints[curLine][0].Y].setPixelState(true);
                lastPoint = linePoints[curLine][linePoints[curLine].Count - 1];
                gameField.grid[(int)lastPoint.X, (int)lastPoint.Y].setPixelState(true);
              
                prevPoint = null;
                textField.text = ds[curLine][iteration].ToString();
            }
        }
        else
        {
            prevPoint = linePoints[curLine][iteration];

            if (invoker.X == prevPoint.X && invoker.Y == prevPoint.Y)
            {
                Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 100);
                invoker.setPixelState(true);
                iteration++;
                textField.text = ds[curLine][iteration].ToString();
                prevPoint = linePoints[curLine][iteration];
            }
            else
                Messenger.Broadcast(GameEvents.ACTION_WRONG_ANSWER);
        }
    }

    public override void DoRestartAction()
    {
        gameField.ClearGrid();

        curLine = 0;
        iteration = 0;
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
        linePoints = new List<Position>[linesCount];

        foreach (var line in lines)
        {
            var linePoints = Algorithms.GetBrezenheimLineData(line, out var ds);
            var i = lines.IndexOf(line);
            this.ds[i] = new List<int>();
            this.linePoints[i] = new List<Position>();
            for (var j = 0; j < ds.Count; j++)
            {
                this.ds[i].Add(ds[j]);
            }
            for (var j = 0; j < linePoints.Count; j++)
            {
                this.linePoints[i].Add(linePoints[j]);
            }
        }
        lastPoint = linePoints[0][linePoints[0].Count - 1];
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
