using Leopotam.Ecs;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class UpdateStopwatchesSystem : IEcsRunSystem 
    {
        private EcsFilter<Stopwatch, Counting> _filter;

        void IEcsRunSystem.Run() 
        {
            foreach(var index in _filter)
            {
                var stopwatch = _filter.Get1(index);
                stopwatch.currentTime += Time.deltaTime;
            }
        }
    }
}