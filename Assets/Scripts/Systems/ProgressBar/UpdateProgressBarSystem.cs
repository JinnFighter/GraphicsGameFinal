using Leopotam.Ecs;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class UpdateProgressBarSystem : IEcsRunSystem
    {
        private EcsFilter<CorrectAnswerEvent> _correctAnswerEventFilter;
        private EcsFilter<ProgressBarComponent> _progressBarFilter;
        
        void IEcsRunSystem.Run()
        {
            if(!_correctAnswerEventFilter.IsEmpty())
            {
                foreach(var index in _progressBarFilter)
                {
                    ref var progressBarComponent = ref _progressBarFilter.Get1(index);
                    var progressBar = progressBarComponent.ProgressBar;
                    progressBar.CurrentValue++;
                    var percentage = progressBar.CurrentValue / progressBar.MaxValue * 100;
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

                    progressBar.Color = color;
                }
            }
        }
    }
}