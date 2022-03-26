using System.Collections.Generic;
using UnityEngine;

namespace Configurations.Script
{
    [CreateAssetMenu(fileName = "ColorPickerConfigs", menuName = "Configs/ColorPickerConfigs")]
    public class ColorPickerConfigs : ScriptableObject
    {
        [field: SerializeField] public List<ColorPickerConfig> Configs { get; private set; }
        public ColorPickerConfig this[int difficulty] => Configs[difficulty];
    }

    [CreateAssetMenu(fileName = "ColorPickerConfig", menuName = "Configs/ColorPickerConfig")]
    public class ColorPickerConfig : ScriptableObject
    {
        [field: SerializeField] public int ColorCount { get; private set; } = 5;
    }
}
