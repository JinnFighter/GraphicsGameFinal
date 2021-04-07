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
            return new List<(Vector2Int, Vector2Int)>();
        }
    }
}
