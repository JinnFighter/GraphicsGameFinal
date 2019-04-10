using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BezierGameController : MonoBehaviour
{
    private List<GridPixelScript> curvePoints;
    // Start is called before the first frame update
    void Start()
    {
        curvePoints = new List<GridPixelScript>();
        GridPixelScript a = new GridPixelScript();
        a.X = 0;
        a.Y = 0;
        GridPixelScript b = new GridPixelScript();
        b.X = 8;
        b.Y = 0;
        GridPixelScript c = new GridPixelScript();
        c.X = 5;
        c.Y = 8;
        curvePoints.Add(a);
        curvePoints.Add(c);
        curvePoints.Add(b);
        drawBezier();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void drawBezier()
    {
        double t, sx, sy, oldx, oldy, ax, ay, tau;
        oldx = curvePoints[0].X;
        oldy = curvePoints[0].Y;
        int counter = curvePoints.Count;
        for (t = 0; t <= 0.5; t += 0.005)
        {
            sx = curvePoints[0].X;
            sy = curvePoints[0].Y;
            ax = 1.0;
            ay = 1.0;
            tau = 1.0;
            for (int i = 1; i < counter; i++)//counter;
            {
                tau = tau * (1 - t);
                ax = ax * t * (counter - i) / (i * (1 - t));
                ay = ay * t * (counter - i) / (i * (1 - t));
                sx = sx + ax * curvePoints[i].X;
                sy = sy + ay * curvePoints[i].Y;
            }
            sx = sx * tau;
            sy = sy * tau;
            //lineTo;
            GetComponent<BrezenheimGameController>().drawLine((int)(oldx), (int)(oldy), (int)(sx), (int)(sy));
            //Point F = new Point(Convert.ToInt32(oldx), Convert.ToInt32(oldy));
            //Point G = new Point(Convert.ToInt32(sx), Convert.ToInt32(sy));
            //getCurve(F, G, Color.Red);

            oldx = sx;
            oldy = sy;
        }
        oldx = curvePoints[counter - 1].X;
        oldy = curvePoints[counter - 1].Y;
        for (t = 1.0; t >= 0.5; t = t - 0.005)
        {
            sx = curvePoints[counter - 1].X;
            sy = curvePoints[counter - 1].Y;
            ax = 1.0;
            ay = 1.0;
            tau = 1.0;
            for (int i = counter - 2; i >= 0; i--)
            {
                tau = tau * t;
                ax = ax * (1 - t) * (i + 1) / (t * (counter - 1 - i));
                ay = ay * (1 - t) * (i + 1) / (t * (counter - 1 - i));
                sx = sx + ax * curvePoints[i].X;
                sy = sy + ay * curvePoints[i].Y;
            }
            sx = sx * tau;
            sy = sy * tau;

            GetComponent<BrezenheimGameController>().drawLine((int)(oldx), (int)(oldy), (int)(sx), (int)(sy));
            //Point F = new Point(Convert.ToInt32(oldx), Convert.ToInt32(oldy));
            //Point G = new Point(Convert.ToInt32(sx), Convert.ToInt32(sy));
            //getCurve(F, G, Color.Red);
            oldx = sx;
            oldy = sy;
        }


        //lineTo;
        //hasBezier = true;
    }
    /*void drawBezier()
        {
            double t, sx, sy, oldx, oldy,ax,ay,tau;
            oldx= curvePoints.ElementAt(0).X;
            oldy= curvePoints.ElementAt(0).Y;
            int counter = curvePoints.Count;
            for (t=0;t<=0.5;t+=0.005)
            {
                sx = curvePoints.ElementAt(0).X;
                sy = curvePoints.ElementAt(0).Y;
                ax = 1.0;
                ay = 1.0;
                tau = 1.0;
                for(int i=1;i<counter;i++)//counter;
                {
                    tau = tau * (1 - t);
                    ax = ax * t * (counter - i) / (i * (1 - t));
                    ay = ay * t * (counter - i) / (i * (1 - t));
                    sx = sx + ax * curvePoints.ElementAt(i).X;
                    sy = sy + ay * curvePoints.ElementAt(i).Y;
                }
                sx = sx * tau;
                sy = sy * tau;
                //lineTo;
                Point F=new Point(Convert.ToInt32(oldx), Convert.ToInt32(oldy));
                Point G = new Point(Convert.ToInt32(sx), Convert.ToInt32(sy));
                getCurve(F, G, Color.Red);

                oldx = sx;
                oldy = sy;
            }
            oldx = curvePoints.ElementAt(counter - 1).X;
            oldy = curvePoints.ElementAt(counter - 1).Y;
            for (t=1.0;t>=0.5;t=t-0.005)
            {
                sx = curvePoints.ElementAt(counter -1).X;
                sy = curvePoints.ElementAt(counter - 1).Y;
                ax = 1.0;
                ay = 1.0;
                tau = 1.0;
                for (int i = counter - 2;i>= 0;i--)
                {
                    tau = tau * t;
                    ax = ax * (1 - t) * (i + 1) / (t * (counter - 1 - i));
                    ay = ay * (1 - t) * (i + 1) / (t * (counter - 1 - i));
                    sx = sx + ax * curvePoints.ElementAt(i).X;
                    sy = sy + ay * curvePoints.ElementAt(i).Y;
                }
                sx = sx * tau;
                sy = sy * tau;
                Point F = new Point(Convert.ToInt32(oldx), Convert.ToInt32(oldy));
                Point G = new Point(Convert.ToInt32(sx), Convert.ToInt32(sy));
                getCurve(F, G, Color.Red);
                oldx=sx;
                oldy=sy;
            }


            //lineTo;
            hasBezier = true;
        }
     */
}
