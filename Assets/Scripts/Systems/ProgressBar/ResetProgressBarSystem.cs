using Leopotam.Ecs;
using System.Linq;

namespace Pixelgrid {
    public sealed class ResetProgressBarSystem : IEcsRunSystem 
    {
        private EcsFilter<RestartGameEvent> _restartEventFilter;
        private EcsFilter<ProgressBarComponent> _progressBarFilter;
        private EcsFilter<LineData> _lineDataFilter;

        void IEcsRunSystem.Run()
        {
            if(!_restartEventFilter.IsEmpty())
            {
                ref var lineData = ref _lineDataFilter.Get1(0);
                foreach(var index in _progressBarFilter)
                {
                    ref var progressBarComponent = ref _progressBarFilter.Get1(index);
                    var progressBar = progressBarComponent.ProgressBar;
                    progressBar.MaxValue = lineData.LinePoints.Sum(linePoint => linePoint.Count);
                    progressBar.SetProgress(0);
                    progressBar.Color = UnityEngine.Color.red;
                }
            }
        }
    }
}