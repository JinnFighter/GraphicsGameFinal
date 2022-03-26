using Configurations.Script;
using Leopotam.Ecs;
using Pixelgrid.DataModels;

namespace Pixelgrid.Systems.GameModes.ColorPicker 
{
    public sealed class CreateColorDataSystem : IEcsInitSystem 
    {
        private readonly DifficultyConfiguration _difficultyConfiguration = null;
        private readonly ColorPickerDataModel _colorPickerDataModel = null;
        private readonly ColorPickerConfigs _colorPickerConfigs = null;

        public void Init()
        {
            _colorPickerDataModel.ColorCount = _colorPickerConfigs[_difficultyConfiguration.Difficulty].ColorCount;
        }
    }
}