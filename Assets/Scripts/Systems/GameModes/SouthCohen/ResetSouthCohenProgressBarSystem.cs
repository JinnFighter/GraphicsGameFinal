using Leopotam.Ecs;
using System.Linq;

namespace Pixelgrid 
{
    public sealed class ResetSouthCohenProgressBarSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<RestartGameEvent> _restartEventFilter = null;
        private readonly EcsFilter<SouthCohenData> _southCohenDataFilter = null;

        private readonly ProgressBar _progressBar = null;

        void IEcsRunSystem.Run()
        {
            if (!_restartEventFilter.IsEmpty())
            {
                foreach (var index in _southCohenDataFilter)
                {
                    var southCohenData = _southCohenDataFilter.Get1(index);
                    _progressBar.MaxValue = southCohenData.Zones.Sum(zonePart => zonePart.Count);
                    _progressBar.CurrentValue = 0;
                }
            }
        }
    }
}