using UnityEngine;

namespace Pixelgrid.Configurations.Script
{
    [CreateAssetMenu(fileName = "GameFieldConfig", menuName = "Configs/GameFieldConfig")]
    public class GameFieldConfig : ScriptableObject
    {
        [field: SerializeField] public int FieldSize { get; private set; } = 10;
    }
}
