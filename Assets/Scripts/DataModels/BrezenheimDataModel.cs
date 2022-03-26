using System.Collections.Generic;
using UnityEngine;

namespace Pixelgrid.DataModels
{
    public class BrezenheimDataModel
    {
        public readonly List<List<int>> Indexes = new List<List<int>>();
        public List<List<Vector2Int>> LinePoints = new List<List<Vector2Int>>();
        public int CurrentPoint;
        public int CurrentLine;
    }
}
