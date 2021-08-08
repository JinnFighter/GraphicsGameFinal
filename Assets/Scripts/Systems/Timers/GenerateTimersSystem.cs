using Leopotam.Ecs;
using UnityEngine.UI;

namespace Pixelgrid 
{
    public sealed class GenerateTimersSystem : IEcsInitSystem 
    {
        private readonly EcsWorld _world = null;
        private readonly TimersContainer _timersContainer = null;
        
        public void Init() 
        {
            var timerEntity = _world.NewEntity();
            timerEntity.Get<Timer>();
            timerEntity.Get<GameplayTimerComponent>();
            ref var timerRef = ref timerEntity.Get<TimerRef>();
            timerRef.TimerFormat = _timersContainer.gameplayTimer.GetComponent<TimerFormat>();
            ref var textRef = ref timerEntity.Get<TextRef>();
            textRef.Text = _timersContainer.gameplayTimer.GetComponent<Text>();
        }
    }
}