using Leopotam.Ecs;
using Pixelgrid.Components.Time.Timer;

namespace Pixelgrid.Systems.Timers 
{
    public sealed class SetGameplayTimerStartTimeSystem : IEcsRunSystem 
    {
        private readonly DifficultyConfiguration _difficultyConfiguration = null;
        private readonly EcsFilter<Timer, GameplayTimerComponent> _filter = null;
        private readonly EcsFilter<RestartGameEvent> _restartEventFilter = null;

        public void Run()
        {
            if (!_restartEventFilter.IsEmpty())
            {
                var difficulty = _difficultyConfiguration.Difficulty;
                float startTime;
                switch (difficulty)
                {
                    case 1:
                        startTime = 80f;
                        break;
                    case 2:
                        startTime = 120f;
                        break;
                    default:
                        startTime = 60f;
                        break;
                }

                foreach (var index in _filter)
                {
                    ref var timer = ref _filter.Get1(index);
                    timer.startTime = startTime;
                    timer.currentTime = startTime;

                    var entity = _filter.GetEntity(index);
                    ref var timeChangeEvent = ref entity.Get<TimeChangeEvent>();
                    timeChangeEvent.CurrentTime = timer.currentTime;
                }
            }
        }
    }
}