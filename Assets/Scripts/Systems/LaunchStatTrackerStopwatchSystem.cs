using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class LaunchStatTrackerStopwatchSystem : IEcsRunSystem 
    {
        private EcsFilter<GameplayEventReceiver, StartGameEvent> _startFilter;
        private EcsFilter<Stopwatch, StatTrackerStopwatch> _statTrackerFilter;

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