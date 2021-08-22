using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;
using UnityEngine.EventSystems;

namespace Pixelgrid
{
    public class CheckRestartClickSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EcsUiClickEvent> _filter = null;
        private readonly EcsFilter<GameplayEventReceiver> _eventReceiverFilter = null;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var data = ref _filter.Get1(index);
                if (data.Button == PointerEventData.InputButton.Left && data.Sender.CompareTag("RestartButton"))
                {
                    foreach(var eventFilterIndex in _eventReceiverFilter)
                    {
                        var entity = _eventReceiverFilter.GetEntity(eventFilterIndex);
                        entity.Get<RestartGameEvent>();
                    }
                }
            }
        }
    }
}
