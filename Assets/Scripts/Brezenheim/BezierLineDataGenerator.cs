using System;

public class BezierLineDataGenerator : ILineDataGenerator
{
    private int _curveLinesCount;

    public BezierLineDataGenerator(int curveLinesCount)
    {
        _curveLinesCount = curveLinesCount;
    }

    public LinesModeData GenerateData(int minLength, int maxLength)
    {
        var linesData = new LinesModeData();
        for (var i = 0; i < _curveLinesCount; i++)
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

        return linesData;
    }
}
