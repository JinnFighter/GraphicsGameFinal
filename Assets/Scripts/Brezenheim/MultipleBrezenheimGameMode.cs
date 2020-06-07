using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class MultipleBrezenheimGameMode : GameMode
{
    private Pixel[,] lines;
    private int linesQuantity;
    private List<Pixel>[] linePoints;
    private List<int>[] ds;
    private Pixel last_point;
    private Pixel prev_point;
    private int cur_line;
    private int iteration;
    private int minLineLength;
    private int maxLineLength;
    private InputField textField;
    private GameField _gameField;

    public MultipleBrezenheimGameMode(GameplayTimer timer, int diff, GameField inputField, InputField nextTextField) : base(timer, diff)
    {
        textField = nextTextField;
        _gameField = inputField;
        difficulty = diff;
        gameActive = false;
        gameStarted = false;
        switch (difficulty)
        {
            case 0:
                linesQuantity = 5;
                minLineLength = 2;
                maxLineLength = 5;
                break;
            case 1:
                linesQuantity = 7;
                minLineLength = 4;
                maxLineLength = 8;
                break;
            case 2:
                linesQuantity = 10;
                minLineLength = 5;
                maxLineLength = 10;
                break;
            default:
                linesQuantity = 5;
                minLineLength = 2;
                maxLineLength = 5;
                break;
        }
        lines = new Pixel[2, linesQuantity];
        linePoints = new List<Pixel>[linesQuantity];
        ds = new List<int>[linesQuantity];
        for (var i = 0; i < linesQuantity; i++)
        {
            linePoints[i] = new List<Pixel>();
            ds[i] = new List<int>();
        }
        Messenger<Pixel>.AddListener(GameEvents.GAME_CHECK, CheckAction);
        Messenger.AddListener(GameEvents.TIMER_STOP, ChangeGameState);
        Messenger.AddListener(GameEvents.PAUSE_GAME, Pause);
        Messenger.AddListener(GameEvents.CONTINUE_GAME, Continue);
        Messenger.AddListener(GameEvents.RESTART_GAME, Restart);
        GeneratePolygon();

        last_point = linePoints[0][linePoints[0].Count - 1];
        lines[0, 0].setPixelState(true);
        lines[1, 0].setPixelState(true);
        textField.text = ds[0][0].ToString();

        eventReactor = new DefaultReactor(timer, difficulty);

        Messenger.Broadcast(GameEvents.START_GAME);
    }
    
    ~MultipleBrezenheimGameMode()
    {
        Messenger<Pixel>.RemoveListener(GameEvents.GAME_CHECK, CheckAction);
        Messenger.RemoveListener(GameEvents.TIMER_STOP, ChangeGameState);
        Messenger.RemoveListener(GameEvents.PAUSE_GAME, Pause);
        Messenger.RemoveListener(GameEvents.CONTINUE_GAME, Continue);
        Messenger.RemoveListener(GameEvents.RESTART_GAME, Restart);
    }

    public override void ChangeGameState()
    {
        if (!gameStarted)
        {
            gameActive = true;
            gameStarted = true;
            eventReactor.OnChangeState(difficulty);
        }
        else
            gameActive = false;
    }

    public override void CheckAction(Pixel invoker)
    {
         if (!gameActive) return;

            //if (!timer.Counting) return;

            if (cur_line == linesQuantity)
            {
                Messenger.Broadcast(GameEvents.TIMER_STOP);
                return;
            }

            if (prev_point == last_point)
            {
                cur_line++;
                if (cur_line == linesQuantity)
                {
                    eventReactor.OnGameOver();
                    return;
                }
                else
                {
                    iteration = 0;
                    _gameField.clearGrid();
                    Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 100);
                    lines[0, cur_line].setPixelState(true);
                    lines[1, cur_line].setPixelState(true);
                    last_point = linePoints[cur_line][linePoints[cur_line].Count - 1];
                    prev_point = null;
                    textField.text = ds[cur_line][iteration].ToString();
                }
            }
            else
            {
                prev_point = linePoints[cur_line][iteration];

                if (invoker == prev_point)
                {
                    Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 100);
                    invoker.setPixelState(true);
                    iteration++;
                    textField.text = ds[cur_line][iteration].ToString();
                    prev_point = linePoints[cur_line][iteration];
                }
                else
                    Messenger.Broadcast(GameEvents.ACTION_WRONG_ANSWER);
            }
    }

    public override void Restart()
    {
        gameActive = false;
        gameStarted = false;
        _gameField.clearGrid();
        for (var i = 0; i < linesQuantity; i++)
        {
            ds[i].Clear();
            linePoints[i].Clear();
        }
        cur_line = 0;
        iteration = 0;

        GeneratePolygon();
        last_point = linePoints[0][linePoints[0].Count - 1];
        lines[0, 0].setPixelState(true);
        lines[1, 0].setPixelState(true);
        eventReactor.OnRestart();
        Messenger.Broadcast(GameEvents.START_GAME);
    }

    public void GeneratePolygon()
    {
        for (var i = 0; i < linesQuantity; i++)
        {
            int firstX;
            int firstY;

            int secondX;
            int secondY;

            if (i == 0)
            {
                firstX = UnityEngine.Random.Range(0, 9);
                firstY = UnityEngine.Random.Range(0, 9);

                secondX = UnityEngine.Random.Range(0, 9);
                secondY = UnityEngine.Random.Range(0, 9);

                while (GetLineLength(firstX, firstY, secondX, secondY) > maxLineLength
                    || GetLineLength(firstX, firstY, secondX, secondY) < minLineLength)
                {
                    secondX = UnityEngine.Random.Range(0, 9);
                    secondY = UnityEngine.Random.Range(0, 9);
                }
            }
            else
            {
                firstX = lines[1, i - 1].Y;
                firstY = lines[1, i - 1].X;
                if (i == linesQuantity - 1)
                {
                    secondX = lines[0, 0].Y;
                    secondY = lines[0, 0].X;

                    while (true)
                    {
                        bool check = false;
                        for (var j = 0; j < i - 1; j++)
                        {
                            if (Algorithms.GetSegmentIntersection(lines[0, j], lines[1, j],
                                _gameField.grid[firstY, firstX], _gameField.grid[secondY, secondX]))
                            {
                                break;
                            }
                        }
                        if (check)
                            continue;
                        else
                            break;
                    }
                }
                else
                {
                    while (true)
                    {
                        secondX = UnityEngine.Random.Range(0, 9);
                        secondY = UnityEngine.Random.Range(0, 9);
                        if (GetLineLength(firstX, firstY, secondX, secondY) > maxLineLength
                        || GetLineLength(firstX, firstY, secondX, secondY) <= minLineLength - 1)
                        {
                            continue;
                        }

                        bool check = false;
                        for (var j = 0; j < i - 1; j++)
                        {
                            if (Algorithms.GetSegmentIntersection(lines[0, j], linePoints[j][linePoints[j].Count - 2],
                             _gameField.grid[firstY, firstX], _gameField.grid[secondY, secondX]))
                            {
                                break;
                            }
                        }

                        if (check)
                            continue;
                        else
                            break;
                    }
                }
            }

            lines[0, i] = _gameField.grid[firstY, firstX];
            lines[1, i] = _gameField.grid[secondY, secondX];
            Algorithms.GetBrezenheimLineData(_gameField, firstX, firstY, secondX, secondY, out ds[i], out linePoints[i]);
        }
    }

    private double GetLineLength(int x0, int y0, int x1, int y1) => Math.Sqrt((x1 - x0) * (x1 - x0) + (y1 - y0) * (y1 - y0));
}
