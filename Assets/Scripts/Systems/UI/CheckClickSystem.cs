using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;
using UnityEngine.EventSystems;

namespace Pixelgrid 
{
    public sealed class CheckClickSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<EcsUiClickEvent> _filter = null;
        
        void IEcsRunSystem.Run() 
        {
            foreach (var index in _filter)
            {
                ref var data = ref _filter.Get1(index);
                var sender = data.Sender;
                if (data.Button == PointerEventData.InputButton.Left && sender.TryGetComponent<GridPixel>(out var pixel))
                    pixel.entity.Get<PixelClickedEvent>();
            }
        }
    }
}