using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class ResetColorPickerProgressBarSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<RestartGameEvent> _restartEventFilter = null;
        private readonly EcsFilter<ColorPickerData> _colorPickerFilter = null;

        private readonly ProgressBar _progressBar = null;

        void IEcsRunSystem.Run()
        {
            if (!_restartEventFilter.IsEmpty())
            {
                foreach (var index in _colorPickerFilter)
                {
                    var colorPickerData = _colorPickerFilter.Get1(index);
                    _progressBar.MaxValue = colorPickerData.ColorCount;
                    _progressBar.CurrentValue = 0;
                }
            }
        }
    }
}