using UnityEngine;

namespace Pixelgrid.ScriptableObjects.Prefabs
{
    [CreateAssetMenu(fileName = "PrefabsContent", menuName = "Content/PrefabsContent")]
    public class PrefabsContent : ScriptableObject
    {
        [field: SerializeField] public GameObject Pixel { get; private set; }
    }
}
