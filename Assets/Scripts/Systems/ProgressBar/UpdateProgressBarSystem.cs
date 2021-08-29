using Leopotam.Ecs;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class UpdateProgressBarSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CorrectAnswerEvent> _correctAnswerEventFilter = null;

        private readonly ProgressBar _progressBar = null;
        
        void IEcsRunSystem.Run()
        {
            if(!_correctAnswerEventFilter.IsEmpty())
            {
                _progressBar.CurrentValue++;
                var percentage = _progressBar.CurrentValue / _progressBar.MaxValue * 100;
                Color color;
                if (percentage < 61)
                    color = new Color32(220, 221, 225, 255);
                else
                if (percentage < 71)
                    color = new Color32(194, 54, 22, 255);
                else
                if (percentage < 91)
                    color = new Color32(251, 197, 49, 255);
                else
                    color = new Color32(68, 189, 50, 255);

                _progressBar.Color = color;
            }
        }
    }
}