using Leopotam.Ecs;
using Pixelgrid.Components.UI.ProgressBar;

namespace Pixelgrid.Systems.UI.ProgressBar
{
    public class DestroyProgressBarViewSystem : IEcsDestroySystem
    {
        private readonly EcsFilter<ProgressBarViewModel, ProgressBarView> _filter = null;
        
        public void Destroy()
        {
            foreach (var index in _filter)
            {
                var entity = _filter.GetEntity(index);
                entity.Del<ProgressBarViewModel>();
                entity.Del<ProgressBarView>();
            }
        }
    }
}
