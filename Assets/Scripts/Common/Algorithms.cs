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

    public static List<Position> GetBrezenheimLine(Line line)
    {
        var res = new List<Position>();

        var start = line.GetStart();
        var end = line.GetEnd();
        int x0 = (int)start.Y;
        int y0 = (int)start.X;
        int x1 = (int)end.Y;
        int y1 = (int)end.X;
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

            res.Add(new Position(x0, y0));

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

                res.Add(new Position(x, y));
                x += sx;
            }
        }
        else
        {
            var d = (dx << 1) - dy;
            var d1 = dx << 1;
            var d2 = (dx - dy) << 1;

            res.Add(new Position(x0, y0));

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

                res.Add(new Position(x, y));
                y += sy;
            }
        }

        return res;
    }

    public static void GetBrezenheimLineData(Line line, out List<int> ds, out List<Position> linePoints)
    {
        ds = new List<int>();
        linePoints = new List<Position>();
        var start = line.GetStart();
        var end = line.GetEnd();
        var x0 = (int)start.Y;
        var y0 = (int)start.X;
        var x1 = (int)end.Y;
        var y1 = (int)end.X;

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

                linePoints.Add(new Position(x, y));
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

                linePoints.Add(new Position(x, y));
                ds.Add(d);

                y += sy;
            }
        }
    }
}