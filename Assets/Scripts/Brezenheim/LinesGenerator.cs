using System.Collections.Generic;
using UnityEngine;

namespace Pixelgrid
{
    public abstract class LinesGenerator : MonoBehaviour, ILinesDataGenerator
    {
        public abstract IEnumerable<(Vector2Int, Vector2Int)> GenerateData(int minLength, int maxLength, int count);
    }
}
