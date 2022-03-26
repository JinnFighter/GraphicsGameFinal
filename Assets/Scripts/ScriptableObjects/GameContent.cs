using Pixelgrid.ScriptableObjects.Audio;
using Pixelgrid.ScriptableObjects.Prefabs;
using Pixelgrid.ScriptableObjects.Sprites;
using UnityEngine;

namespace Pixelgrid.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameContent", menuName = "Content/GameContent")]
    public class GameContent : ScriptableObject
    {
        [field: SerializeField] public AudioContent AudioContent { get; private set; }
        [field: SerializeField] public PrefabsContent PrefabsContent { get; private set; }
        [field: SerializeField] public SpritesContent SpritesContent { get; private set; }
    }
}
