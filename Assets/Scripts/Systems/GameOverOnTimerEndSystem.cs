using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class GameOverOnTimerEndSystem : IEcsRunSystem 
    {
        private EcsFilter<GameplayEventReceiver> _eventReceiversFilter;
        private EcsFilter<GameplayTimerComponent, TimerEndEvent> _filter;

        void IEcsRunSystem.Run() 
        {
            if(!_filter.IsEmpty())
            {
                foreach(var index in _eventReceiversFilter)
                {
                    var entity = _eventReceiversFilter.GetEntity(index);
                    entity.Get<GameOverEvent>();
                }
            }
        }
    }
}