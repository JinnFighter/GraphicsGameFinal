using Leopotam.Ecs;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class UpdateTimersSystem : IEcsRunSystem 
    {
        private EcsFilter<Timer, Counting, TimerRef, TextRef> _filter = null;

        void IEcsRunSystem.Run () 
        {
            foreach (var index in _filter)
            {
                var entity = _filter.GetEntity(index);

                ref var timerData = ref _filter.Get1(index);
                var currentTime = timerData.currentTime -= Time.deltaTime;

                if (currentTime <= 0.000f)
                    currentTime = 0.000f;

                timerData.currentTime = currentTime;

                if (timerData.currentTime <= 0.0000f)
                {
                    entity.Del<Counting>();
                    entity.Get<TimerEndEvent>();
                }

                var timerFormat = _filter.Get3(index);
                ref var updateTextEvent = ref entity.Get<UpdateTextEvent>();
                updateTextEvent.Text = timerFormat.TimerFormat.GetFormattedTime(currentTime); 
            }
        }
    }
}