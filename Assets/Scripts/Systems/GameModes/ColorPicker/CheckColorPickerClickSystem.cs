using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;
using UnityEngine.EventSystems;

namespace Pixelgrid 
{
    public sealed class CheckColorPickerClickSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<EcsUiClickEvent> _filter = null;

        private readonly ImageHolderContainer _imageHolderContainer = null;

        void IEcsRunSystem.Run()
        {
            foreach (var index in _filter)
            {
                ref var data = ref _filter.Get1(index);
                var sender = data.Sender;
                if (data.Button == PointerEventData.InputButton.Left && sender.CompareTag("ColorCheckButton"))
                {
                    var color = _imageHolderContainer.AnswerHolder.color;
                    var entity = _filter.GetEntity(index);
                    ref var chosenColor = ref entity.Get<ColorChosenEvent>();
                    chosenColor.Color = color;
                }
            }
        }
    }
}