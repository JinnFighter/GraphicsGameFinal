using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class DisableStopwatchOnGameOverSystem : IEcsRunSystem 
    {
        private EcsFilter<Stopwatch, Counting> _stopwatchFilter;
        private EcsFilter<GameplayEventReceiver, GameOverEvent> _eventReceiversFilter;

        void IEcsRunSystem.Run() 
        {
            if(!_eventReceiversFilter.IsEmpty())
            {
                foreach(var index in _stopwatchFilter)
                {
                    var entity = _stopwatchFilter.GetEntity(index);
                    entity.Del<Counting>();
                }
            }
        }
    }
}