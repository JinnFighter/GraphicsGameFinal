using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class GenerateLineDataSystem : IEcsInitSystem 
    {
        private EcsFilter<GameModeData> _gameModeDataFilter;

        public void Init() 
        {
            foreach(var index in _gameModeDataFilter)
            {
                var entity = _gameModeDataFilter.GetEntity(index);
                entity.Get<LineData>();
            }
        }
    }
}