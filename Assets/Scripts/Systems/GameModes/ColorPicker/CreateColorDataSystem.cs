using Leopotam.Ecs;
using Pixelgrid.DataModels;

namespace Pixelgrid.Systems.GameModes.ColorPicker 
{
    public sealed class CreateColorDataSystem : IEcsInitSystem 
    {
        private readonly DifficultyConfiguration _difficultyConfiguration = null;
        private readonly ColorPickerDataModel _colorPickerDataModel = null;

        public void Init()
        {
            var colorsCount = _difficultyConfiguration.Difficulty switch
            {
                1 => 7,
                2 => 10,
                _ => 5,
            };

            _colorPickerDataModel.ColorCount = colorsCount;
        }
    }
}