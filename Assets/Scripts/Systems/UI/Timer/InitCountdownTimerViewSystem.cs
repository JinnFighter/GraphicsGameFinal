using Leopotam.Ecs;
using Pixelgrid.Components.UI.Timer;
using Pixelgrid.UI.Views;

namespace Pixelgrid.Systems.UI.Timer
{
    public class InitCountdownTimerViewSystem : IEcsInitSystem
    {
        private readonly EcsFilter<Components.Time.Timer.Timer, CountdownTimer> _filter = null;
        private readonly TimersContainer _timersContainer = null;
        
        public void Init()
        {
            foreach (var index in _filter)
            {
                var entity = _filter.GetEntity(index);
                entity.Get<TimerViewModel>();
                ref var timerView = ref entity.Get<TimerViewComponent>();
                timerView.TimerView = _timersContainer.CountdownTimer.GetComponent<TimerView>();
            }
        }
    }
}
