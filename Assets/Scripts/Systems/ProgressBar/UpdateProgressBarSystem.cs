using Leopotam.Ecs;

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
                    progressBarComponent.ProgressBar.IncrementProgress();
                }
            }
        }
    }
}