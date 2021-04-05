using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class GenerateTimersSystem : IEcsInitSystem 
    {
        private readonly EcsWorld _world;
        private readonly TimersContainer _timersContainer;
        
        public void Init() 
        {
            var timerEntity = _world.NewEntity();
            timerEntity.Get<Timer>();
            timerEntity.Get<GameplayTimerComponent>();
            ref var timerRef = ref timerEntity.Get<TimerRef>();
            timerRef.timer = _timersContainer.gameplayTimer;
        }
    }
}