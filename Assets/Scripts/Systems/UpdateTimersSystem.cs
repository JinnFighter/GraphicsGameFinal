using Leopotam.Ecs;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class UpdateTimersSystem : IEcsRunSystem 
    {
        private EcsFilter<Timer, Counting> _filter;
        
        void IEcsRunSystem.Run () 
        {
            foreach(var index in _filter)
            {
                var timerData = _filter.Get1(index);
                timerData.currentTime -= Time.deltaTime;

                if (timerData.currentTime <= 0.0000f)
                {
                    timerData.currentTime = 0.0000f;
                    var entity = _filter.GetEntity(index);
                    entity.Del<Counting>();
                    entity.Get<TimerEndEvent>();
                }
            }
        }
    }
}