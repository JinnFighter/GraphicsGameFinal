using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    public void Swap<T>(ref T a, ref T b)
    {
        T c = a;
        a = b;
        b = c;
    }

    public void swap<T>( T a,  T b)
    {
        T c = a;
        a = b;
        b = c;
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

        GameField field = gameObject.GetComponent<GameField>();

        if (dy < dx)
        {
            int d = (dy << 1) - dx;
            int d1 = dy << 1;
            int d2 = (dy - dx) << 1;

            field.grid[x0, y0].setPixelState(true);

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

                field.grid[x, y].setPixelState(true);
                x += sx;
            }
        }
        else
        {
            int d = (dx << 1) - dy;
            int d1 = dx << 1;
            int d2 = (dx - dy) << 1;

            field.grid[x0, y0].setPixelState(true);

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

                field.grid[x, y].setPixelState(true);
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
        GameField field = GetComponent<GameField>();
        while (x <= y)
        {
            field.grid[x + xc, y + yc].setPixelState(true);
            field.grid[x + xc, -y + yc].setPixelState(true);
            field.grid[-x + xc, -y + yc].setPixelState(true);
            field.grid[-x + xc, y + yc].setPixelState(true);
            field.grid[y + xc, x + yc].setPixelState(true);
            field.grid[y + xc, -x + yc].setPixelState(true);
            field.grid[-y + xc, -x + yc].setPixelState(true);
            field.grid[-y + xc, x + yc].setPixelState(true);

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

            drawLine((int)(oldx), (int)(oldy), (int)(sx), (int)(sy));

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

            drawLine((int)(oldx), (int)(oldy), (int)(sx), (int)(sy));

            oldx = sx;
            oldy = sy;
        }

        //hasBezier = true;
    }

    public int Code(GridPixelScript point, GridPixelScript rectLeft, GridPixelScript rectRight)
    {
        int code = 0;
        if (point.X < rectLeft.X) code |= 0x01;//_ _ _ 1;
        if (point.X > rectRight.X) code |= 0x04;//_ 1 _ _;
        if (point.Y < rectLeft.Y) code |= 0x02;//_ _ 1 _;
        if (point.Y > rectRight.Y) code |= 0x08;//1 _ _ _;
        return code;
    }

    public void southCohen(GridPixelScript nA, GridPixelScript nB, GridPixelScript rectLeft, GridPixelScript rectRight)
    {
        GridPixelScript A = nA;
        GridPixelScript B = nB;
        int code1 = this.Code(A, rectLeft, rectRight);
        int code2 = this.Code(B, rectLeft, rectRight);
        bool inside = (code1 | code2) == 0;
        bool outside = (code1 & code2) != 0;
        while (!inside && !outside)
        {
            if (code1 == 0)
            {
                Swap(ref A, ref B);
                int c = code1;
                code1 = code2;
                code2 = c;
            }
            if (Convert.ToBoolean(code1 & 0x01))
            {
                A.Y += (rectLeft.X - A.X) * (B.Y - A.Y) / (B.X - A.X);
                A.X = rectLeft.X;
            }
            if (Convert.ToBoolean(code1 & 0x02))
            {
                A.X += (rectLeft.Y - A.Y) * (B.X - A.X) / (B.Y - A.Y);
                A.Y = rectLeft.Y;
            }
            if (Convert.ToBoolean(code1 & 0x04))
            {
                A.Y += (rectRight.X - A.X) * (B.Y - A.Y) / (B.X - A.X);
                A.X = rectRight.X;
            }
            if (Convert.ToBoolean(code1 & 0x08))
            {
                A.X += (rectRight.Y - A.Y) * (B.X - A.X) / (B.Y - A.Y);
                A.Y = rectRight.Y;
            }

            code1 = Code(A, rectLeft, rectRight);
            inside = (code1 | code2) == 0;
            outside = (code1 & code2) != 0;

        }
        if (!outside)
            drawLine(A.X, A.Y, B.X, B.Y);
    }

    public bool segmentIntersection(GridPixelScript a, GridPixelScript b, GridPixelScript c, GridPixelScript d)
    {
        int v1= (d.X - c.X) * (a.Y - c.Y) - (d.Y - c.Y) * (a.X - c.X);
        int v2= (d.X - c.X) * (b.Y - c.Y) - (d.Y - c.Y) * (b.X - c.X);
        int v3= (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);
        int v4= (b.X - a.X) * (d.Y - a.Y) - (b.Y - a.Y) * (d.X - a.X);
        return (v1 * v2 < 0) && (v3 * v4 < 0);
    }

    public bool segIntersect(GridPixelScript a, GridPixelScript b, GridPixelScript c, GridPixelScript d)
    {
        GridPixelScript A = a;
        GridPixelScript B = b;
        GridPixelScript C = c;
        GridPixelScript D = d;

        if(A.X>B.X)
        {
            swap( A, B);
        }

        if(C.X>D.X)
        {
            swap(C, D);
        }

        float k1;
        float k2;

        if(A.X==B.X)
        {
            k1 = 0;
        }
        else
        {
            k1 = (B.X - A.X) / (B.Y - A.Y);
        }

        if (C.X == D.X)
        {
            k2 = 0;
        }
        else
        {
            k2 = (D.X - C.X) / (D.Y - C.Y);
        }
        
        if(k1==k2)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}