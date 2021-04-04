using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class CheckClickSystem : IEcsRunSystem 
    {
        private EcsFilter<EcsUiClickEvent> _filter;

        void IEcsRunSystem.Run() 
        {
            foreach (var index in _filter)
            {
                ref EcsUiClickEvent data = ref _filter.Get1(index);
                Debug.Log("Im clicked!", data.Sender);
            }
        }
    }
}