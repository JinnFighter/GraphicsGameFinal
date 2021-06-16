using System.Collections.Generic;
using UnityEngine;

namespace Pixelgrid
{
    public class PolygonLinesGenerator : LinesGenerator
    {
        public override IEnumerable<(Vector2Int, Vector2Int)> GenerateData(int minLength, int maxLength, int count)
        {
            var lines = new List<(Vector2Int, Vector2Int)>();
            var linePoints = new List<Vector2Int>[count];
            for (var i = 0; i < count; i++)
            {
                linePoints[i] = new List<Vector2Int>();
                int x0;
                int y0;

                int x1;
                int y1;

                if (i == 0)
                {
                    x0 = Random.Range(0, 9);
                    y0 = Random.Range(0, 9);

                    x1 = Random.Range(0, 9);
                    y1 = Random.Range(0, 9);

                    var line = new Line(new Position(x0, y0), new Position(x1, y1));
                    var lineLength = line.GetLength();
                    while (lineLength > maxLength
                        || lineLength < minLength)
                    {
                        x1 = Random.Range(0, 9);
                        y1 = Random.Range(0, 9);
                        var end = line.GetEnd();
                        end.X = x1;
                        end.Y = y1;
                        lineLength = line.GetLength();
                    }
                }
                else
                {
                    x0 = lines[i - 1].Item2.x;
                    y0 = lines[i - 1].Item2.y;
                    if (i == count - 1)
                    {
                        x1 = lines[0].Item1.x;
                        y1 = lines[0].Item1.y;
                    }
                    else
                    {
                        while (true)
                        {
                            x1 = Random.Range(0, 9);
                            y1 = Random.Range(0, 9);
                            var line = new Line(new Position(x0, y0), new Position(x1, y1));
                            var lineLength = line.GetLength();
                            if (lineLength > maxLength
                            || lineLength <= minLength - 1)
                            {
                                var end = line.GetEnd();
                                end.X = x1;
                                end.Y = y1;
                                continue;
                            }

                            bool check = false;
                            for (var j = 0; j < i - 1; j++)
                            {
                                if (HasSegmentsIntersection(lines[j].Item1, linePoints[j][linePoints[j].Count - 2],
                                 new Vector2Int(x0, y0), new Vector2Int(y1, x1)))
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
                var lineStart = new Vector2Int(x0, y0);
                var lineEnd = new Vector2Int(x1, y1);
                lines.Add((lineStart, lineEnd));
                linePoints[i] = Algorithms.GetBrezenheimLineData(lineStart, lineEnd, out _);
            }

            return lines;
        }

        private bool HasSegmentsIntersection(Vector2Int a, Vector2Int b, Vector2Int c, Vector2Int d)
        {
            int v1 = (d.x - c.x) * (a.y - c.y) - (d.y - c.y) * (a.x - c.x);
            int v2 = (d.x - c.x) * (b.y - c.y) - (d.y - c.y) * (b.x - c.x);
            int v3 = (b.x - a.x) * (c.y - a.y) - (b.y - a.y) * (c.x - a.x);
            int v4 = (b.x - a.x) * (d.y - a.y) - (b.y - a.y) * (d.x - a.x);
            return (v1 * v2 < 0) && (v3 * v4 < 0);
        }
    }
}
