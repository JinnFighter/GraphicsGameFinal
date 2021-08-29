using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class StartGameSystem : IEcsInitSystem, IEcsRunSystem 
    {
        private readonly EcsFilter<RestartGameEvent> _restartEventFilter = null;

        private readonly EcsWorld _world = null;

        public void Init() => Execute();

        private void Execute()
        {
            var entity = _world.NewEntity();
            entity.Get<StartGameEvent>();
        }

        public void Run()
        {
            if (!_restartEventFilter.IsEmpty())
                Execute();
        }
    }
}