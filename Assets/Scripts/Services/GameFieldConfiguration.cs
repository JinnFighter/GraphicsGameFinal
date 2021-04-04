using UnityEngine;
using UnityEngine.UI;

namespace Pixelgrid
{   
    [CreateAssetMenu]
    public class GameFieldConfiguration : ScriptableObject
    {
        public int fieldSize = 10;
        public GameObject pixel;
        public GridLayoutGroup grid;
    }
}
