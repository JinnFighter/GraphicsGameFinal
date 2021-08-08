using Leopotam.Ecs;
using UnityEngine.UI;

namespace Pixelgrid {
    public sealed class GenerateCountdownTimersSystem : IEcsInitSystem 
    {
        private readonly EcsWorld _world = null;
        private readonly TimersContainer _timersContainer = null;

        public void Init()
        {
            var timerEntity = _world.NewEntity();
            timerEntity.Get<Timer>();
            timerEntity.Get<CountdownTimer>();
            ref var timerRef = ref timerEntity.Get<TimerRef>();
            timerRef.TimerFormat = _timersContainer.CountdownTimer.GetComponent<TimerFormat>();
            ref var textRef = ref timerEntity.Get<TextRef>();
            textRef.Text = _timersContainer.CountdownTimer.GetComponent<Text>();
        }
    }
}