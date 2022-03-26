using Pixelgrid.Configurations.Script;
using UnityEngine;

namespace Configurations.Script
{
    [CreateAssetMenu(fileName = "GameFieldConfigs", menuName = "Configs/GameFieldConfigs")]
    public class GameFieldConfigs : ScriptableObject
    {
        [field: SerializeField] public GameFieldConfig[] Configs { get; private set; }
        public GameFieldConfig this[int difficulty] => Configs[difficulty];
    }
}
