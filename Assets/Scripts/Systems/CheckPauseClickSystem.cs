using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class CheckPauseClickSystem : IEcsRunSystem 
    {
        private EcsFilter<EcsUiClickEvent> _filter;
        private EcsFilter<GameplayEventReceiver> _eventReceiverFilter;

        void IEcsRunSystem.Run()
        {
            var eventReceiver = _eventReceiverFilter.GetEntity(0);
            if (eventReceiver.Has<PauseEvent>())
                eventReceiver.Del<PauseEvent>();
            else
            {
                foreach (var index in _filter)
                {
                    ref EcsUiClickEvent data = ref _filter.Get1(index);
                    if (data.Sender.CompareTag("PauseButton"))
                    {
                        eventReceiver.Get<PauseEvent>();
                        Debug.Log("CLICKED PAUSE");
                    }
                }
            }
        }
    }
}