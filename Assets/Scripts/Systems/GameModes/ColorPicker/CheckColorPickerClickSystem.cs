using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;

namespace Pixelgrid 
{
    public sealed class CheckColorPickerClickSystem : IEcsRunSystem 
    {
        private EcsFilter<EcsUiClickEvent> _filter = null;
        private EcsFilter<GameplayEventReceiver> _eventReceiverFilter = null;

        private ImageHolderContainer _imageHolderContainer;

        void IEcsRunSystem.Run()
        {
            foreach (var index in _filter)
            {
                ref EcsUiClickEvent data = ref _filter.Get1(index);
                var sender = data.Sender;
                if (sender.CompareTag("ColorCheckButton"))
                {
                    var color = _imageHolderContainer.AnswerHolder.color;
                    foreach (var eventIndex in _eventReceiverFilter)
                    {
                        var entity = _eventReceiverFilter.GetEntity(eventIndex);
                        ref var chosenColor = ref entity.Get<ColorChosenEvent>();
                        chosenColor.Color = color;
                    }
                }
            }
        }
    }
}