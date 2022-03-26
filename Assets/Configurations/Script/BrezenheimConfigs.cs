using System.Collections.Generic;
using UnityEngine;

namespace Configurations.Script
{
    [CreateAssetMenu(fileName = "BrezenheimConfigs", menuName = "Configs/BrezenheimConfigs")]
    public class BrezenheimConfigs : ScriptableObject
    {
        [field: SerializeField] public List<BrezenheimConfig> Configs { get; private set; }
    }

    [CreateAssetMenu(fileName = "BrezenheimConfig", menuName = "Configs/BrezenheimConfig")]
    public class BrezenheimConfig : ScriptableObject
    {
        [field: SerializeField] public int MinLineLength { get; private set; } = 3;
        [field: SerializeField] public int MaxLineLength { get; private set; } = 6;
        [field: SerializeField] public int LineCount { get; private set; } = 5;
    }
}
