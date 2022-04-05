using Leopotam.Ecs;
using Pixelgrid.Components.UI.Timer;

namespace Pixelgrid.Systems.UI.Timer
{
    public class UpdateTimerViewSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Components.Time.Timer.Timer, TimerViewModel, TimerViewComponent> _filter = null;
        
        public void Run()
        {
            foreach (var index in _filter)
            {
                var timer = _filter.Get1(index);
                ref var model = ref _filter.Get2(index);
                var view = _filter.Get3(index);

                if (timer.currentTime != model.CurrentTime)
                {
                    model.CurrentTime = timer.currentTime;
                    view.TimerView.UpdateTime(model.CurrentTime);
                }
            }
        }
    }
}
