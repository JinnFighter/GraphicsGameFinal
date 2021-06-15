using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class ResetColorPickerProgressBarSystem : IEcsRunSystem 
    {
        private EcsFilter<RestartGameEvent> _restartEventFilter;
        private EcsFilter<ProgressBarComponent> _progressBarFilter;
        private EcsFilter<ColorPickerData> _colorPickerFilter;

        void IEcsRunSystem.Run()
        {
            if (!_restartEventFilter.IsEmpty())
            {
                ref var colorPickerData = ref _colorPickerFilter.Get1(0);
                foreach (var index in _progressBarFilter)
                {
                    ref var progressBarComponent = ref _progressBarFilter.Get1(index);
                    var progressBar = progressBarComponent.ProgressBar;
                    progressBar.MaxValue = colorPickerData.ColorCount;
                    progressBar.CurrentValue = 0;
                }
            }
        }
    }
}