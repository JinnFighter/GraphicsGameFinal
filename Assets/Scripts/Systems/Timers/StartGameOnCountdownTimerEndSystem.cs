using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class StartGameOnCountdownTimerEndSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<Timer, CountdownTimer, TimerEndEvent> _timerFilter = null;

        private readonly EcsWorld _world = null;
        private readonly CountdownScreenPresenter _countdownPresenter = null;
        
        void IEcsRunSystem.Run() 
        {
            if (!_timerFilter.IsEmpty())
            {
                _countdownPresenter.gameObject.SetActive(false);
                var entity = _world.NewEntity();
                entity.Get<StartGameEvent>();
            }    
        }
    }
}