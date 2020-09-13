using System.Collections.Generic;

public class BezierLinePointsGenerator : ILinePointsGenerator
{
    public List<Position> Generate(LinesModeData linesData)
    {
        var points = new List<Position>();

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

            points.AddRange(Algorithms.GetBrezenheimLineData(new Line(new Position(oldx, oldy), new Position(sx, sy)), out _));

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

            points.AddRange(Algorithms.GetBrezenheimLineData(new Line(new Position(oldx, oldy), new Position(sx, sy)), out _));

            oldx = sx;
            oldy = sy;
        }

        return points;
    }
}
