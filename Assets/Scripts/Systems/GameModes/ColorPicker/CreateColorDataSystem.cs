using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class CreateColorDataSystem : IEcsInitSystem 
    {
        private readonly EcsFilter<GameModeData> _filter;

        private readonly DifficultyConfiguration _difficultyConfiguration;

        public void Init()
        {
            foreach (var index in _filter)
            {
                var entity = _filter.GetEntity(index);
                ref var data = ref entity.Get<ColorPickerData>();
                var colorsCount = _difficultyConfiguration.Difficulty switch
                {
                    1 => 7,
                    2 => 10,
                    _ => 5,
                };

                data.ColorCount = colorsCount;
            }
        }
    }
}