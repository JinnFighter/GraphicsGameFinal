using Leopotam.Ecs;
using Pixelgrid.Components.Time.Timer;
using UnityEngine;

namespace Pixelgrid.Systems.Timers 
{
    public sealed class UpdateTimersSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<Timer, Counting> _filter = null;

        void IEcsRunSystem.Run() 
        {
            foreach (var index in _filter)
            {
                var entity = _filter.GetEntity(index);

                ref var timerData = ref _filter.Get1(index);
                var currentTime = timerData.currentTime -= Time.deltaTime;

                if (currentTime <= 0.000f)
                    currentTime = 0.000f;

                timerData.currentTime = currentTime;

                ref var timeChangeEvent = ref entity.Get<TimeChangeEvent>();
                timeChangeEvent.CurrentTime = timerData.currentTime;

                if (timerData.currentTime <= 0.0000f)
                {
                    entity.Del<Counting>();
                    entity.Get<TimerEndEvent>();
                }
            }
        }
    }
}