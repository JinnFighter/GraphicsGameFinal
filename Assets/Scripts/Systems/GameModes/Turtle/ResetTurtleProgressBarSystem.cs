using Leopotam.Ecs;
using System.Linq;

namespace Pixelgrid 
{
    public sealed class ResetTurtleProgressBarSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<RestartGameEvent> _restartEventFilter = null;
        private readonly EcsFilter<ProgressBarComponent> _progressBarFilter = null;
        private readonly EcsFilter<TurtlePath> _turtlePathFilter = null;

        void IEcsRunSystem.Run()
        {
            if (!_restartEventFilter.IsEmpty())
            {
                ref var turtlePath = ref _turtlePathFilter.Get1(0);
                foreach (var index in _progressBarFilter)
                {
                    ref var progressBarComponent = ref _progressBarFilter.Get1(index);
                    var progressBar = progressBarComponent.ProgressBar;
                    progressBar.MaxValue = turtlePath.Path.Sum(turtlePath => turtlePath.Count);
                    progressBar.CurrentValue = 0;
                }
            }
        }
    }
}