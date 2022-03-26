using UnityEngine;

namespace Pixelgrid.ScriptableObjects.Sprites
{
    [CreateAssetMenu(fileName = "SpritesContent", menuName = "Content/SpritesContent")]
    public class SpritesContent : ScriptableObject
    {
        [field: SerializeField] public TurtleSpritesContent TurtleSpritesContent { get; private set; }
    }
}
