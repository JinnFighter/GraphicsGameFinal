using UnityEngine;

namespace Configurations.Script
{
    [CreateAssetMenu(fileName = "TurtleModeConfig", menuName = "Configs/TurtleModeConfig")]
    public class TurtleModeConfig : ScriptableObject
    {
        public const char ForwardSymbol = 'F';
        public const char TurnLeftSymbol = '+'; 
        public const char TurnRightSymbol = '-';
    }
}
