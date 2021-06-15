using UnityEngine;

namespace Pixelgrid
{   
    public class GameFieldConfiguration : MonoBehaviour
    {
        public int fieldSize = 10;
        public GameObject pixel;
        public FlexibleGridLayout grid;

        public bool IsWithinField(Vector2Int position) => position.x >= 0 && position.x < fieldSize && position.y >= 0 && position.y < fieldSize;
    }
}
