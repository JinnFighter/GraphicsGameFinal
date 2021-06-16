using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class ResetStopwatchTimeSystem : IEcsRunSystem 
    {
        private EcsFilter<Stopwatch> _filter;
        private EcsFilter<RestartGameEvent> _restartEventFilter;

        public void Run()
        {
            if(!_restartEventFilter.IsEmpty())
            {
                foreach(var index in _filter)
                {
                    var entity = _filter.GetEntity(index);
                    if (entity.Has<Counting>())
                        entity.Del<Counting>();
                    ref var stopwatch = ref _filter.Get1(index);
                    stopwatch.currentTime = 0;
                }
            }
        }
    }
}