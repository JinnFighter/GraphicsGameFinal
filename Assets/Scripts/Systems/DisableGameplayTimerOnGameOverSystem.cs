using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class DisableGameplayTimerOnGameOverSystem : IEcsRunSystem 
    {
        private EcsFilter<GameplayTimerComponent, Counting> _timerFilter;
        private EcsFilter<GameplayEventReceiver, GameOverEvent> _eventReceiversFilter;

        void IEcsRunSystem.Run() 
        {
            if(!_eventReceiversFilter.IsEmpty())
            {
                foreach(var index in _timerFilter)
                {
                    var entity = _timerFilter.GetEntity(index);
                    entity.Del<Counting>();
                }
            }
        }
    }
}