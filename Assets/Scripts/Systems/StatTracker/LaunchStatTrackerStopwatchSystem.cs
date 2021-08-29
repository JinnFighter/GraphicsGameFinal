using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class LaunchStatTrackerStopwatchSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<StartGameEvent> _startFilter = null;
        private readonly EcsFilter<Stopwatch, StatTrackerStopwatch> _statTrackerFilter = null;

        void IEcsRunSystem.Run() 
        {
            if(!_startFilter.IsEmpty())
            {
                foreach(var index in _statTrackerFilter)
                {
                    var entity = _statTrackerFilter.GetEntity(index);
                    entity.Get<Counting>();
                }
            }
        }
    }
}