using Leopotam.Ecs;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class UpdateStopwatchesSystem : IEcsRunSystem 
    {
        private EcsFilter<Stopwatch, Counting> _filter;
        private EcsFilter<GameplayEventReceiver> _pauseFilter;

        void IEcsRunSystem.Run() 
        {
            var eventReceiver = _pauseFilter.GetEntity(0);
            if (!eventReceiver.Has<PauseEvent>())
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