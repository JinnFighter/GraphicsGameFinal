using Leopotam.Ecs;
using System.Linq;

namespace Pixelgrid 
{
    public sealed class ResetSouthCohenProgressBarSystem : IEcsRunSystem 
    {
        private EcsFilter<RestartGameEvent> _restartEventFilter;
        private EcsFilter<ProgressBarComponent> _progressBarFilter;
        private EcsFilter<SouthCohenData> _southCohenDataFilter;

        void IEcsRunSystem.Run()
        {
            if (!_restartEventFilter.IsEmpty())
            {
                ref var southCohenData = ref _southCohenDataFilter.Get1(0);
                foreach (var index in _progressBarFilter)
                {
                    ref var progressBarComponent = ref _progressBarFilter.Get1(index);
                    var progressBar = progressBarComponent.ProgressBar;
                    progressBar.MaxValue = southCohenData.Zones.Sum(zonePart => zonePart.Count);
                    progressBar.CurrentValue = 0;
                }
            }
        }
    }
}