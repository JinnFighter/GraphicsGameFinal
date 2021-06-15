using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class ResetColorsDataSystem : IEcsRunSystem 
    {
        private EcsFilter<RestartGameEvent> _filter;
        private EcsFilter<ColorPickerData> _dataFilter;

        void IEcsRunSystem.Run() 
        {
            if(!_filter.IsEmpty())
            {
                foreach(var index in _dataFilter)
                {
                    ref var data = ref _dataFilter.Get1(index);
                    data.CurrentColor = 0;
                }
            }
        }
    }
}