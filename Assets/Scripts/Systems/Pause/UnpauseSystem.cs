using Leopotam.Ecs;

namespace Pixelgrid
{
    public sealed class UnpauseSystem : IEcsRunSystem
    {
        private readonly EcsFilter<UnpauseEvent> _filter = null;
        private readonly EcsFilter<Paused> _pausedFilter = null;

        void IEcsRunSystem.Run()
        {
            if (!_filter.IsEmpty())
            {
                var enableSystemsEntity = _filter.GetEntity(0);
                ref var enableSystemsEvent = ref enableSystemsEntity.Get<EnableSystemTypeEvent>();
                enableSystemsEvent.SystemType = "Pausable";
                
                foreach (var index in _pausedFilter)
                {
                    var entity = _pausedFilter.GetEntity(index);
                    entity.Del<Paused>();
                }
            }
        }
    }
}