using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class StartGameOnCountdownTimerEndSystem : IEcsRunSystem 
    {
        private EcsFilter<Timer, CountdownTimer, TimerEndEvent> _timerFilter;
        private EcsFilter<GameplayEventReceiver> _eventReceiverFilter;

        void IEcsRunSystem.Run () 
        {
            if (!_timerFilter.IsEmpty())
                foreach (var index in _eventReceiverFilter)
                    _eventReceiverFilter.GetEntity(index).Get<StartGameEvent>();
        }
    }
}