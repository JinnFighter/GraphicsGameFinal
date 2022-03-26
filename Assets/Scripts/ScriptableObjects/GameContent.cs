using Pixelgrid.ScriptableObjects.Audio;
using UnityEngine;

namespace Pixelgrid.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameContent", menuName = "Content/GameContent")]
    public class GameContent : ScriptableObject
    {
        [field: SerializeField] public AudioContent AudioContent { get; private set; }
    }
}
