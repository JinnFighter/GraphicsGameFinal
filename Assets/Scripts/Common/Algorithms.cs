using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorithms : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void drawLine(int X0, int Y0, int X1, int Y1)
    {
        int x0 = Y0;
        int y0 = X0;
        int x1 = Y1;
        int y1 = X1;
        //Изменения координат
        int dx = (x1 > x0) ? (x1 - x0) : (x0 - x1);
        int dy = (y1 > y0) ? (y1 - y0) : (y0 - y1);
        //Направление приращения
        int sx = (x1 >= x0) ? (1) : (-1);
        int sy = (y1 >= y0) ? (1) : (-1);

        if (dy < dx)
        {
            int d = (dy << 1) - dx;
            int d1 = dy << 1;
            int d2 = (dy - dx) << 1;

           
            GameField a = gameObject.GetComponent<GameField>();
            a.grid[x0, y0].setPixelState(true);
            int x = x0 + sx;
            int y = y0;
            for (int i = 1; i <= dx; i++)
            {
                if (d > 0)
                {
                    d += d2;
                    y += sy;

                }
                else
                    d += d1;   


                a.grid[x, y].setPixelState(true);
                x += sx;

            }
        }
        else
        {
            int d = (dx << 1) - dy;
            int d1 = dx << 1;
            int d2 = (dx - dy) << 1;

            GameField a = gameObject.GetComponent<GameField>();
            a.grid[x0, y0].setPixelState(true);

            int x = x0;
            int y = y0 + sy;
            for (int i = 1; i <= dy; i++)
            {
                if (d > 0)
                {
                    d += d2;
                    x += sx;
                }
                else
                    d += d1;

                a.grid[x0, y0].setPixelState(true);
                y += sy;

            }
        }
    }
    public void Brezenheim4Circle(int Xc, int Yc, int r)
    {
        int xc = Yc;
        int yc = Xc;
        int x, y, d;
        x = 0;
        y = r;
        d = 3 - 2 * y;
        while (x <= y)
        {
            //grid[x+xc,y+yc].setPixelState(true);
            //grid[x+xc,-y+yc].setPixelState(true);
            //grid[-x+xc,-y+yc].setPixelState(true);
            //grid[-x+xc,y+yc].setPixelState(true);
            //grid[y+xc,x+yc].setPixelState(true);
            //grid[y+xc,-x+yc].setPixelState(true);
            //grid[-y+xc,-x+yc].setPixelState(true);
            //grid[-y+xc,x+yc].setPixelState(true);

            GetComponent<GameField>().grid[x + xc, y + yc].setPixelState(true);
            GetComponent<GameField>().grid[x + xc, -y + yc].setPixelState(true);
            GetComponent<GameField>().grid[-x + xc, -y + yc].setPixelState(true);
            GetComponent<GameField>().grid[-x + xc, y + yc].setPixelState(true);
            GetComponent<GameField>().grid[y + xc, x + yc].setPixelState(true);
            GetComponent<GameField>().grid[y + xc, -x + yc].setPixelState(true);
            GetComponent<GameField>().grid[-y + xc, -x + yc].setPixelState(true);
            GetComponent<GameField>().grid[-y + xc, x + yc].setPixelState(true);

            if (d < 0)
            {
                d = d + 4 * x + 6;
            }
            else
            {
                d = d + 4 * (x - y) + 10;
                y--;
            }
            x++;
        }
    }
    public void drawBezier(List<GridPixelScript> curvePoints)
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
}
