using Leopotam.Ecs;
using System.Linq;

namespace Pixelgrid 
{
    public sealed class ResetTurtleProgressBarSystem : IEcsRunSystem 
    {
        private EcsFilter<RestartGameEvent> _restartEventFilter;
        private EcsFilter<ProgressBarComponent> _progressBarFilter;
        private EcsFilter<TurtlePath> _turtlePathFilter;

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