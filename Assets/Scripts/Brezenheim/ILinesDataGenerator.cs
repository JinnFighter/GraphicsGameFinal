using System.Collections.Generic;
using UnityEngine;

namespace Pixelgrid
{
    public interface ILinesDataGenerator
    {
        IEnumerable<Vector2Int> GenerateData(int minLength, int maxLength);
    }
}
