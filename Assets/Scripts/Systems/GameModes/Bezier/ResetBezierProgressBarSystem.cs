using Leopotam.Ecs;
using UnityEngine;

namespace Pixelgrid {
    public sealed class ResetBezierProgressBarSystem : IEcsRunSystem 
    {
        private EcsFilter<RestartGameEvent> _restartEventFilter;
        private EcsFilter<ProgressBarComponent> _progressBarFilter;
        private EcsFilter<BezierLineData> _lineDataFilter;

        void IEcsRunSystem.Run()
        {
            if (!_restartEventFilter.IsEmpty())
            {
                ref var lineData = ref _lineDataFilter.Get1(0);
                foreach (var index in _progressBarFilter)
                {
                    ref var progressBarComponent = ref _progressBarFilter.Get1(index);
                    var progressBar = progressBarComponent.ProgressBar;
                    progressBar.MaxValue = lineData.Points.Count;
                    progressBar.CurrentValue = 0;
                    progressBar.Color = new Color32(220, 221, 225, 255);
                }
            }
        }
    }
}