using System.Collections.Generic;
using UnityEngine;

namespace Pixelgrid 
{
    public struct LineData 
    {
        public List<List<Vector2Int>> LinePoints;
        public int CurrentPoint;
        public int CurrentLine;
    }
}