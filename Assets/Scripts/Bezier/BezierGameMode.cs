using System;
using System.Collections.Generic;
using UnityEngine;

public class BezierGameMode : GameMode
{
    private int minLineLength;
    private int maxLineLength;
    private List<Pixel> curvePoints;
    private int current;
    private int pointsQuantity;
    private GameField _gameField;

    public BezierGameMode(GameplayTimer timer, int difficulty, GameField field) : base(difficulty)
    {
        gameActive = false;
        gameStarted = false;
        _gameField = field;
        difficulty = _gameField.Difficulty;
        switch (difficulty)
        {
            case 0:
                pointsQuantity = 3;
                minLineLength = 3;
                maxLineLength = 5;
                break;
            case 1:
                pointsQuantity = 5;
                minLineLength = 4;
                maxLineLength = 6;
                break;
            case 2:
                pointsQuantity = 7;
                minLineLength = 5;
                maxLineLength = 7;
                break;
            default:
                pointsQuantity = 3;
                minLineLength = 3;
                maxLineLength = 5;
                break;
        }

        curvePoints = new List<Pixel>(pointsQuantity);
        GenerateBezierCurve();
        Algorithms.DrawBezier(_gameField, curvePoints);
        current = 0;

        for (var i = 0; i < curvePoints.Count; i++)
        {
            Debug.Log("CurvePoint: " + curvePoints[i].X + " " + curvePoints[i].Y);
        }

        Messenger.AddListener(GameEvents.PAUSE_GAME, Pause);
        Messenger.AddListener(GameEvents.CONTINUE_GAME, Continue);
        Messenger.AddListener(GameEvents.RESTART_GAME, Restart);
        Messenger.AddListener(GameEvents.TIMER_STOP, ChangeGameState);
        Messenger<Pixel>.AddListener(GameEvents.GAME_CHECK, CheckAction);

        eventReactor = new DefaultReactor(timer, difficulty);

        Messenger.Broadcast(GameEvents.START_GAME);
    }

    public override void ChangeGameState()
    {
        if (gameStarted)
            gameActive = false;
        else
        {
            gameActive = true;
            gameStarted = true;
            eventReactor.OnChangeState(difficulty);
        }
    }

    public override void CheckAction(Pixel invoker)
    {
            if (!gameActive) return;

            //if (!timer.Counting) return;

            if (current == curvePoints.Count)
                return;
            else
            {
                if (invoker == curvePoints[current])
                {
                    Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 100);
                    current++;
                    if (current == curvePoints.Count)
                    {
                        eventReactor.OnGameOver();
                    }
                }
                else
                {
                    Messenger.Broadcast(GameEvents.ACTION_WRONG_ANSWER);
                    Debug.Log("Wrong!");
                }
            }
       
    }

    public override void Restart()
    {
        gameActive = false;
        gameStarted = false;
        _gameField.ClearGrid();
        curvePoints.Clear();

        current = 0;
        GenerateBezierCurve();
        Algorithms.DrawBezier(_gameField, curvePoints);
        current = 0;

        eventReactor.OnRestart();
        Messenger.Broadcast(GameEvents.START_GAME);
    }

    public void GenerateBezierCurve()
    {
        for (var i = 0; i < pointsQuantity; i++)
        {
            var x = UnityEngine.Random.Range(0, 9);
            var y = UnityEngine.Random.Range(0, 9);
            if (i != 0)
            {
                while ((Math.Sqrt((x - curvePoints[i - 1].Y) * (x - curvePoints[i - 1].Y) + (y - curvePoints[i - 1].X) * (y - curvePoints[i - 1].X)) > maxLineLength
              || Math.Sqrt((x - curvePoints[i - 1].Y) * (x - curvePoints[i - 1].Y) + (y - curvePoints[i - 1].X) * (y - curvePoints[i - 1].X)) < minLineLength))

                {
                    x = UnityEngine.Random.Range(0, 9);
                    y = UnityEngine.Random.Range(0, 9);
                }
            }

            curvePoints.Add(_gameField.grid[y, x]);
        }
    }

    public void drawBezier()
    {
        double t, sx, sy, oldx, oldy, ax, ay, tau;
        oldx = curvePoints[0].Y;
        oldy = curvePoints[0].X;
        var counter = curvePoints.Count;
        for (t = 0; t <= 0.5; t += 0.005)
        {
            sx = curvePoints[0].Y;
            sy = curvePoints[0].X;
            ax = 1.0;
            ay = 1.0;
            tau = 1.0;
            for (int i = 1; i < counter; i++)//counter;
            {
                tau *= 1 - t;
                ax = ax * t * (counter - i) / (i * (1 - t));
                ay = ay * t * (counter - i) / (i * (1 - t));
                sx += ax * curvePoints[i].Y;
                sy += ay * curvePoints[i].X;
            }
            sx *= tau;
            sy *= tau;
            Algorithms.DrawLine(_gameField, (int)oldx, (int)oldy, (int)sx, (int)sy);

            oldx = sx;
            oldy = sy;
        }
        oldx = curvePoints[counter - 1].Y;
        oldy = curvePoints[counter - 1].X;
        for (t = 1.0; t >= 0.5; t -= 0.005)
        {
            sx = curvePoints[counter - 1].Y;
            sy = curvePoints[counter - 1].X;
            ax = 1.0;
            ay = 1.0;
            tau = 1.0;
            for (var i = counter - 2; i >= 0; i--)
            {
                tau *= t;
                ax = ax * (1 - t) * (i + 1) / (t * (counter - 1 - i));
                ay = ay * (1 - t) * (i + 1) / (t * (counter - 1 - i));
                sx += ax * curvePoints[i].Y;
                sy += ay * curvePoints[i].X;
            }
            sx *= tau;
            sy *= tau;

            Algorithms.DrawLine(_gameField, (int)oldx, (int)oldy, (int)sx, (int)sy);

            oldx = sx;
            oldy = sy;
        }
    }

    private double GetLineLength(int x0, int y0, int x1, int y1) => Math.Sqrt((x1 - x0) * (x1 - x0) + (y1 - y0) * (y1 - y0));
}
