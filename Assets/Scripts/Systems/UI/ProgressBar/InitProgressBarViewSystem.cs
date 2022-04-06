using Leopotam.Ecs;
using Pixelgrid.Components.UI.ProgressBar;

namespace Pixelgrid.Systems.UI.ProgressBar
{
    public class InitProgressBarViewSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly Pixelgrid.ProgressBar _progressBar = null;
        
        public void Init()
        {
            var entity = _world.NewEntity();
            entity.Get<ProgressBarViewModel>();
            ref var view = ref entity.Get<ProgressBarView>();
            view.ProgressBar = _progressBar;
        }
    }
}
