using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class StartGameOnCountdownTimerEndSystem : IEcsRunSystem 
    {
        private CountdownScreenPresenter _countdownPresenter;

        private EcsFilter<Timer, CountdownTimer, TimerEndEvent> _timerFilter;
        private EcsFilter<GameplayEventReceiver> _eventReceiverFilter;

        void IEcsRunSystem.Run() 
        {
            if (!_timerFilter.IsEmpty())
            {
                _countdownPresenter.gameObject.SetActive(false);
                foreach (var index in _eventReceiverFilter)
                    _eventReceiverFilter.GetEntity(index).Get<StartGameEvent>();
            }    
        }
    }
}