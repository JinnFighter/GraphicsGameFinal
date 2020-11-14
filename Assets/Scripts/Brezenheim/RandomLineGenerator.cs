using System.Collections.Generic;

public class RandomLineGenerator : ILineGenerator
{
    private int _minLength;
    private int _maxLength;
    private int _maxLengthSum;

    public RandomLineGenerator(int minLength, int maxLength, int maxLengthSum)
    {
        _minLength = minLength;
        _maxLength = maxLength;
        _maxLengthSum = maxLengthSum;
    }

    public List<Line> Generate(int count)
    {
        var lines = new List<Line>(count);
        var maxLengthSum = _maxLengthSum;
        for (var i = 0; i < count; i++)
        {
            var x0 = UnityEngine.Random.Range(0, 9);
            var y0 = UnityEngine.Random.Range(0, 9);

            var x1 = UnityEngine.Random.Range(0, 9);
            var y1 = UnityEngine.Random.Range(0, 9);


            var line = new Line(new Position(x0, y0), new Position(x1, y1));
            var lineLength = line.GetLength();
            if (maxLengthSum > 0)
            {
                if (maxLengthSum - (int)lineLength < _minLength
                    && maxLengthSum - (int)lineLength != 0)
                {
                    while (lineLength > _maxLength
               || lineLength < _minLength)
                    {
                        x0 = UnityEngine.Random.Range(0, 9);
                        y0 = UnityEngine.Random.Range(0, 9);

                        x1 = UnityEngine.Random.Range(0, 9);
                        y1 = UnityEngine.Random.Range(0, 9);

                        var start = line.GetStart();
                        start.X = x0;
                        start.Y = y0;
                        var end = line.GetEnd();
                        end.X = x1;
                        end.Y = y1;
                        lineLength = line.GetLength();
                    }
                }

                else
                {
                    while (lineLength > _maxLength
               || lineLength < _minLength)
                    {
                        x0 = UnityEngine.Random.Range(0, 9);
                        y0 = UnityEngine.Random.Range(0, 9);

                        x1 = UnityEngine.Random.Range(0, 9);
                        y1 = UnityEngine.Random.Range(0, 9);

                        var start = line.GetStart();
                        start.X = x0;
                        start.Y = y0;
                        var end = line.GetEnd();
                        end.X = x1;
                        end.Y = y1;
                        lineLength = line.GetLength();
                    }
                }
                maxLengthSum -= (int)lineLength;
            }
            lines.Add(new Line(new Position(y0, x0), new Position(y1, x1)));
        }

        return lines;
    }
}
