using UnityEngine;

namespace Pixelgrid
{
    public class CodeReceiver : MonoBehaviour
    {
        public int GetCode(Vector2Int point, Vector2Int topLeft, Vector2Int downRight)
        {
            var code = 0;

            if (point.x < topLeft.x) code |= 0x01;     //_ _ _ 1;
            if (point.x > downRight.x) code |= 0x04;   //_ 1 _ _;
            if (point.y < topLeft.y) code |= 0x02;     //_ _ 1 _;
            if (point.y > downRight.y) code |= 0x08;   //1 _ _ _;

            return code;
        }
    }
}
