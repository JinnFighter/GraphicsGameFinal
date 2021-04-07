using System.Collections.Generic;
using UnityEngine;

namespace Pixelgrid 
{
    public struct LineData 
    {
        public List<(Vector2Int, Vector2Int)> LinePoints;
        public int CurrentPoint;
    }
}