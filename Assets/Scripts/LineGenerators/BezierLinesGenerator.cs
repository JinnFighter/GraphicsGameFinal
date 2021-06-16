using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pixelgrid
{
    public class BezierLinesGenerator : MonoBehaviour
    {
        public List<Vector2Int> GenerateData(int minLength, int maxLength, int count)
        {
            var linesData = new LinesModeData();
            for (var i = 0; i < count; i++)
            {
                var x = UnityEngine.Random.Range(0, 9);
                var y = UnityEngine.Random.Range(0, 9);
                if (i != 0)
                {
                    var point = linesData.GetPoint(i - 1);
                    while ((Math.Sqrt((x - point.Y) * (x - point.Y) + (y - point.X) * (y - point.X)) > maxLength
                  || Math.Sqrt((x - point.Y) * (x - point.Y) + (y - point.X) * (y - point.X)) < minLength))

                    {
                        x = UnityEngine.Random.Range(0, 9);
                        y = UnityEngine.Random.Range(0, 9);
                    }
                }

                linesData.AddPoint(new Position(y, x));
            }

            var points = new List<Vector2Int>();

            var firstPoint = linesData.GetPoint(0);
            double t, sx, sy, oldx, oldy, ax, ay, tau;
            oldx = firstPoint.X;
            oldy = firstPoint.Y;
            var counter = linesData.GetPointsCount();

            for (t = 0; t <= 0.5; t += 0.005)
            {
                sx = firstPoint.X;
                sy = firstPoint.Y;
                ax = 1.0;
                ay = 1.0;
                tau = 1.0;
                for (int i = 1; i < counter; i++)//counter;
                {
                    tau *= (1 - t);
                    ax = ax * t * (counter - i) / (i * (1 - t));
                    ay = ay * t * (counter - i) / (i * (1 - t));
                    sx += ax * linesData.GetPoint(i).X;
                    sy += ay * linesData.GetPoint(i).Y;
                }
                sx *= tau;
                sy *= tau;

                points.AddRange(Algorithms.GetBrezenheimLineData(new Vector2Int((int)oldx, (int)oldy), new Vector2Int((int)sx, (int)sy), out _));

                oldx = sx;
                oldy = sy;
            }
            oldx = linesData.GetPoint(counter - 1).X;
            oldy = linesData.GetPoint(counter - 1).Y;
            for (t = 1.0; t >= 0.5; t -= 0.005)
            {
                sx = linesData.GetPoint(counter - 1).X;
                sy = linesData.GetPoint(counter - 1).Y;
                ax = 1.0;
                ay = 1.0;
                tau = 1.0;
                for (var i = counter - 2; i >= 0; i--)
                {
                    tau *= t;
                    ax = ax * (1 - t) * (i + 1) / (t * (counter - 1 - i));
                    ay = ay * (1 - t) * (i + 1) / (t * (counter - 1 - i));
                    sx += ax * linesData.GetPoint(i).X;
                    sy += ay * linesData.GetPoint(i).Y;
                }
                sx *= tau;
                sy *= tau;

                points.AddRange(Algorithms.GetBrezenheimLineData(new Vector2Int((int)oldx, (int)oldy), new Vector2Int((int)sx, (int)sy), out _));

                oldx = sx;
                oldy = sy;
            }

            return points;
        }
    }
}
