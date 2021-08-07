using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace Pixelgrid 
{
    public sealed class UpdateTimersSystem : IEcsRunSystem 
    {
        private EcsFilter<Timer, Counting, TimerRef> _filter;

        void IEcsRunSystem.Run () 
        {
            foreach (var index in _filter)
            {
                ref var timerData = ref _filter.Get1(index);
                var currentTime = timerData.currentTime -= Time.deltaTime;

                if (currentTime <= 0.000f)
                    currentTime = 0.000f;

                timerData.currentTime = currentTime;

                var timerRef = _filter.Get3(index);
                var timerGameObject = timerRef.timer;
                var timerText = timerGameObject.GetComponent<Text>();
                var timerFormat = timerGameObject.GetComponent<TimerFormat>();
                timerText.text = timerFormat.GetFormattedTime(currentTime);
                if (timerData.currentTime <= 0.0000f)
                {
                    var entity = _filter.GetEntity(index);
                    entity.Del<Counting>();
                    entity.Get<TimerEndEvent>();
                }
            }
        }
    }
}