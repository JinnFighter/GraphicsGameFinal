using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class SetGameplayTimerStartTimeSystem : IEcsRunSystem, ICommand 
    {
        private DifficultyConfiguration _difficultyConfiguration;
        private EcsFilter<Timer, GameplayTimerComponent> _filter;
        private EcsFilter<RestartGameEvent> _restartEventFilter;

        public void Execute()
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
            }
        }

        public void Run()
        {
            if (!_restartEventFilter.IsEmpty())
                Execute();
        }
    }
}