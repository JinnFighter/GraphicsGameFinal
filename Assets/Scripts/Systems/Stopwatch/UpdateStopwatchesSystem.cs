using Leopotam.Ecs;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class UpdateStopwatchesSystem : IEcsRunSystem 
    {
        private EcsFilter<Stopwatch, Counting> _filter = null;

        void IEcsRunSystem.Run() 
        {
            foreach (var index in _filter)
            {
                ref var stopwatch = ref _filter.Get1(index);
                stopwatch.currentTime += Time.deltaTime;
            }  
        }
    }
}