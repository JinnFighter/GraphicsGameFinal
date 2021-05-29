using Leopotam.Ecs;

namespace Pixelgrid {
    public sealed class GenerateCountdownTimersSystem : IEcsInitSystem 
    {
        private readonly EcsWorld _world;
        private readonly TimersContainer _timersContainer;

        public void Init()
        {
            var timerEntity = _world.NewEntity();
            timerEntity.Get<Timer>();
            timerEntity.Get<CountdownTimer>();
            ref var timerRef = ref timerEntity.Get<TimerRef>();
            timerRef.timer = _timersContainer.CountdownTimer;
        }
    }
}