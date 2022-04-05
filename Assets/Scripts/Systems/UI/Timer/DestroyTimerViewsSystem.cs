using Leopotam.Ecs;
using Pixelgrid.Components.UI.Timer;

namespace Pixelgrid.Systems.UI.Timer
{
    public class DestroyTimerViewsSystem : IEcsDestroySystem
    {
        private readonly EcsFilter<TimerViewModel, TimerViewComponent> _filter = null;
        
        public void Destroy()
        {
            foreach (var index in _filter)
            {
                var entity = _filter.GetEntity(index);
                entity.Del<TimerViewModel>();
                entity.Del<TimerViewComponent>();
            }
        }
    }
}
