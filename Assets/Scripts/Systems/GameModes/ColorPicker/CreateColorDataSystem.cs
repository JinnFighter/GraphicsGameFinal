using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class CreateColorDataSystem : IEcsInitSystem 
    {
        private readonly EcsFilter<GameModeData> _filter;

        public void Init()
        {
            foreach (var index in _filter)
            {
                var entity = _filter.GetEntity(index);
                entity.Get<ColorPickerData>();
            }
        }
    }
}