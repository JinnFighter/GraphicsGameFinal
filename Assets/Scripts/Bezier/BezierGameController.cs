using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class BezierGameController : MonoBehaviour
{
    private bool gameActive;
    private bool gameStarted;
    private int difficulty;
    private int minLineLength;
    private int maxLineLength;
    private List<GridPixelScript> curvePoints;
    private int current;
    private int pointsQuantity;

    // Start is called before the first frame update
    void Start()
    {
        gameActive = false;
        gameStarted = false;
        difficulty = GetComponent<GameField>().Difficulty;
        switch(difficulty)
        {
            case 0:
                pointsQuantity = 3;
                minLineLength=3;
                maxLineLength=5;
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
        
        curvePoints = new List<GridPixelScript>(pointsQuantity);
        GenerateBezierCurve();
        drawBezier();
        current = 0;

        for(int i=0;i<curvePoints.Count;i++)
        {
            Debug.Log("CurvePoint: " + curvePoints[i].X + " " + curvePoints[i].Y);
        }
        GetComponent<GameplayTimer>().Format = GameplayTimer.TimerFormat.smms;
        Messenger.AddListener(GameEvents.PAUSE_GAME, PauseGame);
        Messenger.AddListener(GameEvents.CONTINUE_GAME, ContinueGame);
        Messenger.AddListener(GameEvents.RESTART_GAME, RestartGame);
        Messenger.AddListener(GameEvents.TIMER_STOP, ChangeGameState);
        Messenger<GridPixelScript>.AddListener(GameEvents.GAME_CHECK, gameCheck);
        Messenger.Broadcast(GameEvents.START_GAME);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvents.PAUSE_GAME, PauseGame);
        Messenger.RemoveListener(GameEvents.CONTINUE_GAME, ContinueGame);
        Messenger.RemoveListener(GameEvents.RESTART_GAME, RestartGame);
        Messenger.RemoveListener(GameEvents.TIMER_STOP, ChangeGameState);
        Messenger<GridPixelScript>.RemoveListener(GameEvents.GAME_CHECK, gameCheck);
    }
    public void drawBezier()
    {
        double t, sx, sy, oldx, oldy, ax, ay, tau;
        oldx = curvePoints[0].Y;
        oldy = curvePoints[0].X;
        int counter = curvePoints.Count;
        Algorithms algorithms = GetComponent<Algorithms>();
        for (t = 0; t <= 0.5; t += 0.005)
        {
            sx = curvePoints[0].Y;
            sy = curvePoints[0].X;
            ax = 1.0;
            ay = 1.0;
            tau = 1.0;
            for (int i = 1; i < counter; i++)//counter;
            {
                tau = tau * (1 - t);
                ax = ax * t * (counter - i) / (i * (1 - t));
                ay = ay * t * (counter - i) / (i * (1 - t));
                sx = sx + ax * curvePoints[i].Y;
                sy = sy + ay * curvePoints[i].X;
            }
            sx = sx * tau;
            sy = sy * tau;
            algorithms.drawLine((int)(oldx), (int)(oldy), (int)(sx), (int)(sy));

            oldx = sx;
            oldy = sy;
        }
        oldx = curvePoints[counter - 1].Y;
        oldy = curvePoints[counter - 1].X;
        for (t = 1.0; t >= 0.5; t = t - 0.005)
        {
            sx = curvePoints[counter - 1].Y;
            sy = curvePoints[counter - 1].X;
            ax = 1.0;
            ay = 1.0;
            tau = 1.0;
            for (int i = counter - 2; i >= 0; i--)
            {
                tau = tau * t;
                ax = ax * (1 - t) * (i + 1) / (t * (counter - 1 - i));
                ay = ay * (1 - t) * (i + 1) / (t * (counter - 1 - i));
                sx = sx + ax * curvePoints[i].Y;
                sy = sy + ay * curvePoints[i].X;
            }
            sx = sx * tau;
            sy = sy * tau;

            algorithms.drawLine((int)(oldx), (int)(oldy), (int)(sx), (int)(sy));

            oldx = sx;
            oldy = sy;
        }
    }
    public void gameCheck(GridPixelScript invoker)
    {
        if (!gameActive)
        {
            return;
        }
        if (!GetComponent<GameplayTimer>().Counting)
        {
            return;
        }
        if (current == curvePoints.Count)
        {
            return;
        }
        else
        {
            if(invoker == curvePoints[current])
            {
                Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 100);
                current++;
                if(current == curvePoints.Count)
                {
                    GetComponent<GameplayTimer>().StopTimer();
                    Messenger.Broadcast(GameEvents.GAME_OVER);
                }
            }
            else
            {
                Messenger.Broadcast(GameEvents.ACTION_WRONG_ANSWER);
                Debug.Log("Wrong!");
            }
        }
    }

    public void GenerateBezierCurve()
    {
        GameField field = GetComponent<GameField>();
        for(int i=0;i<pointsQuantity;i++)
        {
            int x = UnityEngine.Random.Range(0, 9);
            int y = UnityEngine.Random.Range(0, 9);
            if(i!=0)
            {
                while ((Math.Sqrt((x - curvePoints[i-1].Y) * (x - curvePoints[i - 1].Y) + (y - curvePoints[i - 1].X) * (y - curvePoints[i - 1].X)) > maxLineLength
              || Math.Sqrt((x - curvePoints[i - 1].Y) * (x - curvePoints[i - 1].Y) + (y - curvePoints[i - 1].X) * (y - curvePoints[i - 1].X)) < minLineLength))
              
                {
                    x= UnityEngine.Random.Range(0, 9);
                    y= UnityEngine.Random.Range(0, 9);
                }
            }

            curvePoints.Add(field.grid[y,x]);
        }
    }
    public void ChangeGameState()
    {
        if (!gameStarted)
        {
            gameActive = true;
            gameStarted = true;
            GetComponent<GameplayTimer>().StartTime = 60f;
            GetComponent<GameplayTimer>().StartTimer();
        }
        else
        {
            gameActive = false;
        }
    }
    public void PauseGame()
    {
        if(gameStarted)
        {
            gameActive = false;
            GetComponent<GameplayTimer>().PauseTimer();
        }
        
    }
    public void ContinueGame()
    {
        if(gameStarted)
        {
            gameActive = true;
            GetComponent<GameplayTimer>().ResumeTimer();
        }
    }
    public void RestartGame()
    {
        gameActive = false;
        gameStarted = false;
        GetComponent<GameField>().clearGrid();
        curvePoints.Clear();

        current = 0;
        GenerateBezierCurve();
        drawBezier();
        current = 0;

        GetComponent<GameplayTimer>().timerText.text = GameplayTimer.TimerFormat.smms_templater_timerText;
        Messenger.Broadcast(GameEvents.START_GAME);
    }
    public void SendStartGameEvent()
    {
        Messenger.Broadcast(GameEvents.START_GAME);
    }
}
