using System.Collections.Generic;
using UnityEngine;

namespace Pixelgrid
{
    public class RandomLinesGenerator : LinesGenerator
    {
        public int MaxLengthSum;

        public RandomLinesGenerator()
        {
            MaxLengthSum = 20;
        }

        public override IEnumerable<(Vector2Int, Vector2Int)> GenerateData(int minLength, int maxLength, int count)
        {
            var lines = new List<(Vector2Int, Vector2Int)>();
            var maxLengthSum = MaxLengthSum;

            for(var i = 0; i < count; i++)
            {
                if(maxLengthSum > 0)
                {
                    var length = 0;
                    int x0 = Random.Range(0, 9);
                    int y0 = Random.Range(0, 9);
                    int x1 = Random.Range(0, 9);
                    int y1 = Random.Range(0, 9);
                    while(length < minLength || length > maxLength)
                    {
                        x0 = Random.Range(0, 9);
                        y0 = Random.Range(0, 9);
                        x1 = Random.Range(0, 9);
                        y1 = Random.Range(0, 9);
                        var line = new NewLine(new Vector2Int(x0, y0), new Vector2Int(x1, y1));
                        length = (int)line.GetLength();
                    }

                    lines.Add((new Vector2Int(x0, y0), new Vector2Int(x1, y1)));
                    maxLengthSum -= length;
                }
            }

            return lines;
        }
    }
}
