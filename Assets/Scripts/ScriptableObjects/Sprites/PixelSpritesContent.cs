using UnityEngine;

namespace Pixelgrid.ScriptableObjects.Sprites
{
    [CreateAssetMenu(fileName = "PixelSpritesContent", menuName = "Content/PixelSpritesContent")]
    public class PixelSpritesContent : ScriptableObject
    {
        [field: SerializeField] public Sprite EmptySprite { get; private set; }
        [field: SerializeField] public Sprite FilledSprite { get; private set; }
        [field: SerializeField] public Sprite LineBeginningSprite { get; private set; }
        [field: SerializeField] public Sprite LineEndSprite { get; private set; }
    }
}
