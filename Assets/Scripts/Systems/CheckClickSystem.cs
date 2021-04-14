using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;

namespace Pixelgrid 
{
    public sealed class CheckClickSystem : IEcsRunSystem 
    {
        private EcsFilter<EcsUiClickEvent> _filter;
        private EcsFilter<GameplayEventReceiver, PauseEvent> _pauseFilter;

        void IEcsRunSystem.Run() 
        {
            if(_pauseFilter.IsEmpty())
            {
                foreach (var index in _filter)
                {
                    ref EcsUiClickEvent data = ref _filter.Get1(index);
                    var sender = data.Sender;
                    var pixel = sender.GetComponent<GridPixel>();
                    if (pixel)
                        pixel.entity.Get<PixelClickedEvent>();
                }
            }
        }
    }
}