using System;
using UnityEngine;

namespace Pixelgrid
{
    public class NewLine
    {
        public Vector2Int Start;
        public Vector2Int End;

        public NewLine(Vector2Int start, Vector2Int end)
        {
            Start = start;
            End = end;
        }

        public double GetLength()
        {
            var xdif = End.x - Start.x;
            var ydif = End.y - Start.y;
            return Math.Sqrt(xdif * xdif + ydif * ydif);
        }
    }
}
