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
                    progressBar.IncrementProgress();
                    var percentage = progressBar.CurrentValue / progressBar.MaxValue * 100;
                    Color color;
                    if (percentage < 71)
                        color = Color.red;
                    else
                        if (percentage < 91)
                            color = Color.yellow;
                                else
                                    color = Color.green;

                    progressBar.Color = color;
                }
            }
        }
    }
}