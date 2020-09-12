using System.Collections.Generic;
using UnityEngine;

public class Algorithms : MonoBehaviour
{
    public static List<Position> GetBrezenheimLineData(Line line, out List<int> ds)
    {
        ds = new List<int>();
        var linePoints = new List<Position>();
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

        return linePoints;
    }
}