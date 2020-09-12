using System.Collections.Generic;

public class PolygonLineGenerator : ILineGenerator
{
    private int _minLength;
    private int _maxLength;

    public PolygonLineGenerator(int minLength, int maxLength)
    {
        _minLength = minLength;
        _maxLength = maxLength;
    }

    public List<Line> Generate(int count)
    {
        var lines = new List<Line>();
        var linePoints = new List<Position>[count];
        for (var i = 0; i < count; i++)
        {
            linePoints[i] = new List<Position>();
            int x0;
            int y0;

            int x1;
            int y1;

            if (i == 0)
            {
                x0 = UnityEngine.Random.Range(0, 9);
                y0 = UnityEngine.Random.Range(0, 9);

                x1 = UnityEngine.Random.Range(0, 9);
                y1 = UnityEngine.Random.Range(0, 9);

                var line = new Line(new Position(x0, y0), new Position(x1, y1));
                var lineLength = line.GetLength();
                while (lineLength > _maxLength
                    || lineLength < _minLength)
                {
                    x1 = UnityEngine.Random.Range(0, 9);
                    y1 = UnityEngine.Random.Range(0, 9);
                    var end = line.GetEnd();
                    end.X = x1;
                    end.Y = y1;
                    lineLength = line.GetLength();
                }
            }
            else
            {
                x0 = (int)lines[i - 1].GetEnd().Y;
                y0 = (int)lines[i - 1].GetEnd().X;
                if (i == count - 1)
                {
                    x1 = (int)lines[0].GetStart().Y;
                    y1 = (int)lines[0].GetStart().X;
                }
                else
                {
                    while (true)
                    {
                        x1 = UnityEngine.Random.Range(0, 9);
                        y1 = UnityEngine.Random.Range(0, 9);
                        var line = new Line(new Position(x0, y0), new Position(x1, y1));
                        var lineLength = line.GetLength();
                        if (lineLength > _maxLength
                        || lineLength <= _minLength - 1)
                        {
                            var end = line.GetEnd();
                            end.X = x1;
                            end.Y = y1;
                            lineLength = line.GetLength();
                            continue;
                        }

                        bool check = false;
                        for (var j = 0; j < i - 1; j++)
                        {
                            if (HasSegmentsIntersection(lines[j].GetStart(), linePoints[j][linePoints[j].Count - 2],
                             new Position(y0, x0), new Position(y1, x1))) ;
                            {
                                break;
                            }
                        }

                        if (check)
                            continue;
                        else
                            break;
                    }
                }
            }

            lines.Add(new Line(new Position(y0, x0), new Position(y1, x1)));
            linePoints[i] = Algorithms.GetBrezenheimLineData(new Line(new Position(x0, y0), new Position(x1, y1)), out _);
        }

        return lines;
    }

    private bool HasSegmentsIntersection(Position a, Position b, Position c, Position d)
    {
        int v1 = (int)((d.X - c.X) * (a.Y - c.Y) - (d.Y - c.Y) * (a.X - c.X));
        int v2 = (int)((d.X - c.X) * (b.Y - c.Y) - (d.Y - c.Y) * (b.X - c.X));
        int v3 = (int)((b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X));
        int v4 = (int)((b.X - a.X) * (d.Y - a.Y) - (b.Y - a.Y) * (d.X - a.X));
        return (v1 * v2 < 0) && (v3 * v4 < 0);
    }
}
