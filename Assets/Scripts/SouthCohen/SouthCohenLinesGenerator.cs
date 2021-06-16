using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pixelgrid
{
    public class SouthCohenLinesGenerator : MonoBehaviour
    {
        public IEnumerable<(Vector2Int, Vector2Int)> GenerateData(int minLength, int maxLength, int maxCoordinate, int count, Vector2Int leftBorderCorner, Vector2Int rightBorderCorner)
        {
            var lines = new List<(Vector2Int, Vector2Int)>();

            for (var i = 0; i < count; i++)
            {
                var firstX = UnityEngine.Random.Range(0, maxCoordinate);
                var firstY = UnityEngine.Random.Range(0, maxCoordinate);

                var secondX = UnityEngine.Random.Range(0, maxCoordinate);
                var secondY = UnityEngine.Random.Range(0, maxCoordinate);

                while ((Math.Sqrt((secondX - firstX) * (secondX - firstX) + (secondY - firstY) * (secondY - firstY)) > maxLength
                  || Math.Sqrt((secondX - firstX) * (secondX - firstX) + (secondY - firstY) * (secondY - firstY)) < minLength)
                  || (!CheckIntersection(firstX, firstY, secondX, secondY,leftBorderCorner, rightBorderCorner)))
                {
                    firstX = UnityEngine.Random.Range(0, maxCoordinate);
                    firstY = UnityEngine.Random.Range(0, maxCoordinate);

                    secondX = UnityEngine.Random.Range(0, maxCoordinate);
                    secondY = UnityEngine.Random.Range(0, maxCoordinate);
                }
                lines.Add((new Vector2Int(firstX, firstY), new Vector2Int(secondX, secondY)));
            }

            return lines;
        }

        private bool CheckIntersection(int Ax, int Ay, int Bx, int By, Vector2Int leftBorderCorner, Vector2Int rightBorderCorner)
        {
            var ax = Ax;
            var ay = Ay;
            var bx = Bx;
            var by = By;
            if (ax > bx)
            {
                Swap(ref ax, ref bx);
                Swap(ref ay, ref by);
            }
            int[,] matr = new int[2, 2];

            matr[0, 0] = ax.CompareTo(leftBorderCorner.x) + ax.CompareTo(rightBorderCorner.x);
            matr[0, 1] = ay.CompareTo(leftBorderCorner.y) + ay.CompareTo(rightBorderCorner.y);
            matr[1, 0] = bx.CompareTo(leftBorderCorner.x) + bx.CompareTo(rightBorderCorner.x);
            matr[1, 1] = by.CompareTo(leftBorderCorner.y) + by.CompareTo(rightBorderCorner.y);
            int checker = matr[0, 0];
            if ((checker == matr[0, 1]) && (checker == matr[1, 0]) && (checker == matr[1, 1]))
                return false;
            else
            {
                var res = (matr[0, 0] * matr[1, 1]) - (matr[1, 0] * matr[0, 1]);
                if (res == 0)
                    return true;
                else
                    return false;
            }
        }

        private void Swap<T>(ref T a, ref T b)
        {
            T c = a;
            a = b;
            b = c;
        }
    }
}
