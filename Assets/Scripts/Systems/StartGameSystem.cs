using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class StartGameSystem : IEcsInitSystem, IEcsRunSystem, ICommand 
    {
        private EcsFilter<GameplayEventReceiver> _filter;
        private EcsFilter<RestartGameEvent> _restartEventFilter;

        public void Init() => Execute();

        public void Execute()
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