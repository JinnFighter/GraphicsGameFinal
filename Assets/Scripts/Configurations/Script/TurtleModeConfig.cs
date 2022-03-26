using UnityEngine;

namespace Configurations.Script
{
    [CreateAssetMenu(fileName = "TurtleModeConfig", menuName = "Configs/TurtleModeConfig")]
    public class TurtleModeConfig : ScriptableObject
    {
        [field: SerializeField] public char ForwardSymbol { get; private set; } = 'F';
        [field: SerializeField] public char TurnLeftSymbol { get; private set; } = '+';
        [field: SerializeField] public char TurnRightSymbol { get; private set; } = '-';
    }
}
