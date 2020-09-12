using System.Collections.Generic;
using UnityEngine;

public class Algorithms : MonoBehaviour
{
    public static void Swap<T>(ref T a, ref T b)
    {
        T c = a;
        a = b;
        b = c;
    }

    public static void swap<T>(T a, T b)
    {
        T c = a;
        a = b;
        b = c;
    }

    public static void DrawLine(GameField field, int X0, int Y0, int X1, int Y1)
    {
        var x0 = Y0;
        var y0 = X0;
        var x1 = Y1;
        var y1 = X1;
        //Изменения координат
        var dx = (x1 > x0) ? (x1 - x0) : (x0 - x1);
        var dy = (y1 > y0) ? (y1 - y0) : (y0 - y1);
        //Направление приращения
        var sx = (x1 >= x0) ? (1) : (-1);
        var sy = (y1 >= y0) ? (1) : (-1);

        if (dy < dx)
        {
            var d = (dy << 1) - dx;
            var d1 = dy << 1;
            var d2 = (dy - dx) << 1;

            field.grid[x0, y0].setPixelState(true);

            var x = x0 + sx;
            var y = y0;
            for (var i = 1; i <= dx; i++)
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
            var d = (dx << 1) - dy;
            var d1 = dx << 1;
            var d2 = (dx - dy) << 1;

            field.grid[x0, y0].setPixelState(true);

            var x = x0;
            var y = y0 + sy;
            for (var i = 1; i <= dy; i++)
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

    public static void GetBrezenheimLineData(GameField gameField, int X0, int Y0, int X1, int Y1, out List<int> ds, out List<Pixel> linePoints)
    {
        ds = new List<int>();
        linePoints = new List<Pixel>();
        var x0 = Y0;
        var y0 = X0;
        var x1 = Y1;
        var y1 = X1;

        //Изменения координат
        var dx = (x1 > x0) ? (x1 - x0) : (x0 - x1);
        var dy = (y1 > y0) ? (y1 - y0) : (y0 - y1);
        //Направление приращения
        var sx = (x1 >= x0) ? 1 : (-1);
        var sy = (y1 >= y0) ? 1 : (-1);

        if (dy < dx)
        {
            var d = (dy << 1) - dx;
            var d1 = dy << 1;
            var d2 = (dy - dx) << 1;

            ds.Add(d);

            var x = x0 + sx;
            var y = y0;
            for (var i = 1; i <= dx; i++)
            {
                if (d > 0)
                {
                    d += d2;
                    y += sy;
                }
                else
                    d += d1;

                linePoints.Add(gameField.grid[x, y]);
                ds.Add(d);
                x += sx;
            }
        }
        else
        {
            var d = (dx << 1) - dy;
            var d1 = dx << 1;
            var d2 = (dx - dy) << 1;

            ds.Add(d);

            var x = x0;
            var y = y0 + sy;
            for (var i = 1; i <= dy; i++)
            {
                if (d > 0)
                {
                    d += d2;
                    x += sx;
                }
                else
                    d += d1;

                linePoints.Add(gameField.grid[x, y]);
                ds.Add(d);

                y += sy;
            }
        }
    }

    public static void DrawBezier(GameField field, List<Pixel> curvePoints)
    {
        double t, sx, sy, oldx, oldy, ax, ay, tau;
        oldx = curvePoints[0].X;
        oldy = curvePoints[0].Y;
        var counter = curvePoints.Count;

        for (t = 0; t <= 0.5; t += 0.005)
        {
            sx = curvePoints[0].X;
            sy = curvePoints[0].Y;
            ax = 1.0;
            ay = 1.0;
            tau = 1.0;
            for (int i = 1; i < counter; i++)//counter;
            {
                tau *= (1 - t);
                ax = ax * t * (counter - i) / (i * (1 - t));
                ay = ay * t * (counter - i) / (i * (1 - t));
                sx += ax * curvePoints[i].X;
                sy += ay * curvePoints[i].Y;
            }
            sx *= tau;
            sy *= tau;

            DrawLine(field,(int)oldx, (int)oldy, (int)sx, (int)sy);

            oldx = sx;
            oldy = sy;
        }
        oldx = curvePoints[counter - 1].X;
        oldy = curvePoints[counter - 1].Y;
        for (t = 1.0; t >= 0.5; t -= 0.005)
        {
            sx = curvePoints[counter - 1].X;
            sy = curvePoints[counter - 1].Y;
            ax = 1.0;
            ay = 1.0;
            tau = 1.0;
            for (var i = counter - 2; i >= 0; i--)
            {
                tau *= t;
                ax = ax * (1 - t) * (i + 1) / (t * (counter - 1 - i));
                ay = ay * (1 - t) * (i + 1) / (t * (counter - 1 - i));
                sx += ax * curvePoints[i].X;
                sy += ay * curvePoints[i].Y;
            }
            sx *= tau;
            sy *= tau;

            DrawLine(field, (int)oldx, (int)oldy, (int)sx, (int)sy);

            oldx = sx;
            oldy = sy;
        }
    }

    public int Code(Pixel point, Pixel rectLeft, Pixel rectRight)
    {
        var code = 0;
        if (point.X < rectLeft.X) code |= 0x01;//_ _ _ 1;
        if (point.X > rectRight.X) code |= 0x04;//_ 1 _ _;
        if (point.Y < rectLeft.Y) code |= 0x02;//_ _ 1 _;
        if (point.Y > rectRight.Y) code |= 0x08;//1 _ _ _;
        return code;
    }

    public static bool GetSegmentIntersection(Pixel a, Pixel b, Pixel c, Pixel d)
    {
        int v1 = (d.X - c.X) * (a.Y - c.Y) - (d.Y - c.Y) * (a.X - c.X);
        int v2 = (d.X - c.X) * (b.Y - c.Y) - (d.Y - c.Y) * (b.X - c.X);
        int v3 = (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);
        int v4 = (b.X - a.X) * (d.Y - a.Y) - (b.Y - a.Y) * (d.X - a.X);
        return (v1 * v2 < 0) && (v3 * v4 < 0);
    }
}