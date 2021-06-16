using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class CreateSouthCohenDataSystem : IEcsInitSystem 
    {
        private EcsFilter<GameModeData> _filter;

        public void Init() 
        {
            foreach(var index in _filter)
            {
                var entity = _filter.GetEntity(index);
                entity.Get<SouthCohenData>();
            }
        }
    }
}