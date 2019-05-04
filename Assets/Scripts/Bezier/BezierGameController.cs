using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BezierGameController : MonoBehaviour
{
    private bool gameActive;
    private bool gameStarted;
    private List<GridPixelScript> curvePoints;
    private int current;
    private int pointsQuantity;
    // Start is called before the first frame update
    void Start()
    {
        gameActive = false;
        gameStarted = false;
        pointsQuantity = 3;
        curvePoints = new List<GridPixelScript>(pointsQuantity);
        GenerateBezierCurve();
        //curvePoints.Add(GetComponent<GameField>().grid[0, 0]);
        //curvePoints.Add(GetComponent<GameField>().grid[5, 8]);
        //curvePoints.Add(GetComponent<GameField>().grid[8, 0]);
        drawBezier();
        current = 0;
        GetComponent<GameplayTimer>().Format = GameplayTimer.TimerFormat.smms;
        Messenger.AddListener(GameEvents.TIMER_STOP, ChangeGameState);
        Messenger<GridPixelScript>.AddListener(GameEvents.GAME_CHECK, gameCheck);
        Messenger.Broadcast(GameEvents.START_GAME);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void drawBezier()
    {
        double t, sx, sy, oldx, oldy, ax, ay, tau;
        oldx = curvePoints[0].Y;
        oldy = curvePoints[0].X;
        int counter = curvePoints.Count;
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
            //lineTo;
            gameObject.GetComponent<Algorithms>().drawLine((int)(oldx), (int)(oldy), (int)(sx), (int)(sy));
            //GetComponent<BrezenheimGameController>().drawLine((int)(oldx), (int)(oldy), (int)(sx), (int)(sy));
            //Point F = new Point(Convert.ToInt32(oldx), Convert.ToInt32(oldy));
            //Point G = new Point(Convert.ToInt32(sx), Convert.ToInt32(sy));
            //getCurve(F, G, Color.Red);

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

            gameObject.GetComponent<Algorithms>().drawLine((int)(oldx), (int)(oldy), (int)(sx), (int)(sy));
            //GetComponent<BrezenheimGameController>().drawLine((int)(oldx), (int)(oldy), (int)(sx), (int)(sy));
            //Point F = new Point(Convert.ToInt32(oldx), Convert.ToInt32(oldy));
            //Point G = new Point(Convert.ToInt32(sx), Convert.ToInt32(sy));
            //getCurve(F, G, Color.Red);
            oldx = sx;
            oldy = sy;
        }


        //lineTo;
        //hasBezier = true;
    }
    public void gameCheck(GridPixelScript invoker)
    {
        if (!GetComponent<GameplayTimer>().Counting)
        {
            Debug.Log("Not Counting due to finish or no start");
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
                Debug.Log("Correct");
                Messenger<int>.Broadcast(GameEvents.ACTION_RIGHT_ANSWER, 100);
                //invoker.setPixelState(true);
                current++;
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
        for(int i=0;i<pointsQuantity;i++)
        {
            int x = UnityEngine.Random.Range(0, 9);
            int y = UnityEngine.Random.Range(0, 9);
            curvePoints.Add(GetComponent<GameField>().grid[y,x]);
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
}
