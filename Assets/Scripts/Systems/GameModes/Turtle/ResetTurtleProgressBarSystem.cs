using Leopotam.Ecs;
using System.Linq;

namespace Pixelgrid 
{
    public sealed class ResetTurtleProgressBarSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<RestartGameEvent> _restartEventFilter = null;
        private readonly EcsFilter<TurtlePath> _turtlePathFilter = null;

        private readonly ProgressBar _progressBar;

        void IEcsRunSystem.Run()
        {
            if (!_restartEventFilter.IsEmpty())
            {
                foreach (var index in _turtlePathFilter)
                {
                    var turtlePath = _turtlePathFilter.Get1(index);
                    _progressBar.MaxValue = turtlePath.Path.Sum(path => path.Count);
                    _progressBar.CurrentValue = 0;
                }
            }
        }
    }
}