using System.Collections.Generic;
using UnityEngine;

namespace Configurations.Script
{
    [CreateAssetMenu(fileName = "SouthCohenConfigs", menuName = "Configs/SouthCohenConfigs")]
    public class SouthCohenConfigs : ScriptableObject
    {
        [field: SerializeField] public List<SouthCohenConfig> Configs { get; private set; }
        public SouthCohenConfig this[int difficulty] => Configs[difficulty];
    }

    [CreateAssetMenu(fileName = "SouthCohenConfig", menuName = "Configs/SouthCohenConfig")]
    public class SouthCohenConfig : ScriptableObject
    {
        [field: SerializeField] public int MinLength { get; private set; } = 6;
        [field: SerializeField] public int MaxLength { get; private set; } = 8;
        [field: SerializeField] public int LinesCount { get; private set; } = 5;
        [field: SerializeField] public int MaxCoordinate { get; private set; } = 9;
    }
}
