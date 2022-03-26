using System.Collections.Generic;
using UnityEngine;

namespace Pixelgrid.DataModels
{
    public class BezierDataModel
    {
        public List<Vector2Int> Points = new List<Vector2Int>();
        public int CurrentPoint;
    }
}
