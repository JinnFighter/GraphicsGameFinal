using System.Collections.Generic;
using UnityEngine;

namespace Configurations.Script
{
    [CreateAssetMenu(fileName = "TurtleConfigs", menuName = "Configs/TurtleConfigs")]
    public class TurtleConfigs : ScriptableObject
    {
        [field: SerializeField] public TurtleModeConfig TurtleModeConfig { get; private set; }
        [field: SerializeField] public List<TurtleConfig> Configs { get; private set; }
        public TurtleConfig this[int difficulty] => Configs[difficulty];
    }

    [CreateAssetMenu(fileName = "TurtleConfig", menuName = "Configs/TurtleConfig")]
    public class TurtleConfig : ScriptableObject
    {
        [field: SerializeField] public int PathLength { get; private set; } = 5;
        [field: SerializeField] public int PathCount { get; private set; } = 5;
        [field: SerializeField] public Vector2Int TurtleStartPosition { get; private set; } = new Vector2Int(4, 4);
    }
}