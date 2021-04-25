using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixelgrid
{
    public class CheckRestartClickSystem : IEcsRunSystem
    {
        private EcsFilter<EcsUiClickEvent> _filter;
        private EcsFilter<GameplayEventReceiver> _eventReceiverFilter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref EcsUiClickEvent data = ref _filter.Get1(index);
                if (data.Sender.CompareTag("RestartButton"))
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
