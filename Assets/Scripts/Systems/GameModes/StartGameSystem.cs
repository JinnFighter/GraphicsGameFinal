using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class StartGameSystem : IEcsInitSystem, IEcsRunSystem 
    {
        private EcsFilter<GameplayEventReceiver> _filter;
        private EcsFilter<RestartGameEvent> _restartEventFilter;

        public void Init() => Execute();

        private void Execute()
        {
            foreach (var index in _filter)
            {
                var entity = _filter.GetEntity(index);
                entity.Get<StartGameEvent>();
            }
        }

        public void Run()
        {
            if (!_restartEventFilter.IsEmpty())
                Execute();
        }
    }
}