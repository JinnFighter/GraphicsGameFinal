using System.Collections.Generic;
using UnityEngine;

namespace Pixelgrid
{
    public class RandomLinesGenerator : ILinesDataGenerator
    {
        private int _maxLengthSum;

        public RandomLinesGenerator(int maxLengthSum)
        {
            _maxLengthSum = maxLengthSum;
        }

        public IEnumerable<(Vector2Int, Vector2Int)> GenerateData(int minLength, int maxLength, int count)
        {
            var lines = new List<(Vector2Int, Vector2Int)>();
            var maxLengthSum = _maxLengthSum;
            for (var i = 0; i < count; i++)
            {
                var x0 = Random.Range(0, 9);
                var y0 = Random.Range(0, 9);

                var x1 = Random.Range(0, 9);
                var y1 = Random.Range(0, 9);


                var line = new NewLine(new Vector2Int(x0, y0), new Vector2Int(x1, y1));
                var lineLength = line.GetLength();
                if (maxLengthSum > 0)
                {

                    while (maxLengthSum - (int)lineLength >= minLength
                        || maxLengthSum - (int)lineLength == 0)
                    {
                        while (lineLength > maxLength
                   || lineLength < minLength)
                        {
                            x0 = Random.Range(0, 9);
                            y0 = Random.Range(0, 9);

                            x1 = Random.Range(0, 9);
                            y1 = Random.Range(0, 9);

                            var start = line.Start;
                            start.x = x0;
                            start.y = y0;
                            var end = line.End;
                            end.x = x1;
                            end.y = y1;
                            lineLength = line.GetLength();
                        }
                    }
                    maxLengthSum -= (int)lineLength;
                }
                lines.Add((new Vector2Int(x0, y0), new Vector2Int(x1, y1)));
            }

            return lines;
        }
    }
}
