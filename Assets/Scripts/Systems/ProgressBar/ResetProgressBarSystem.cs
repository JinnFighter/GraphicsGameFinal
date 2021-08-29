using Leopotam.Ecs;
using System.Linq;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class ResetProgressBarSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<RestartGameEvent> _restartEventFilter = null;
        private readonly EcsFilter<LineData> _lineDataFilter = null;

        private readonly ProgressBar _progressBar = null;

        void IEcsRunSystem.Run()
        {
            if(!_restartEventFilter.IsEmpty())
            {
                foreach (var index in _lineDataFilter)
                {
                    var lineData = _lineDataFilter.Get1(index);
                    _progressBar.MaxValue = lineData.LinePoints.Sum(linePoint => linePoint.Count);
                    _progressBar.CurrentValue = 0;
                    _progressBar.Color = new Color32(220, 221, 225, 255);
                }
            }
        }
    }
}