using UnityEngine;

namespace Pixelgrid
{
    public class UnityRandom : IRandom
    {
        public int Range(int min, int max) => Random.Range(min, max);
    }
}
