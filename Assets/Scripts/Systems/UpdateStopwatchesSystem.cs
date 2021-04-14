using Leopotam.Ecs;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class UpdateStopwatchesSystem : IEcsRunSystem 
    {
        private EcsFilter<Stopwatch, Counting> _filter;
        private EcsFilter<GameplayEventReceiver, PauseEvent> _pauseFilter;

        void IEcsRunSystem.Run() 
        {
            if(_pauseFilter.IsEmpty())
            {
                foreach (var index in _filter)
                {
                    ref var stopwatch = ref _filter.Get1(index);
                    stopwatch.currentTime += Time.deltaTime;
                }
            }   
        }
    }
}