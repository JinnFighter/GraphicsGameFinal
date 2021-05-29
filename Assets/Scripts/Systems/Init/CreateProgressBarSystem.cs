using Leopotam.Ecs;

namespace Pixelgrid
{
    public sealed class CreateProgressBarSystem : IEcsInitSystem 
    {
        private readonly EcsWorld _world;
        private ProgressBar _progressBar;
        
        public void Init() 
        {
            var entity = _world.NewEntity();
            ref var progressBarComponent = ref entity.Get<ProgressBarComponent>();
            progressBarComponent.ProgressBar = _progressBar;
        }
    }
}