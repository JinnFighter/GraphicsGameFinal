using Leopotam.Ecs;

namespace Pixelgrid
{
    public sealed class LaunchGameplayLoopSystem : IEcsInitSystem
    {
        private EcsFilter<GameplayEventReceiver> _filter;

        public void Init()
        {
            foreach(var index in _filter)
            {
                var entity = _filter.GetEntity(index);
                entity.Get<RestartGameEvent>();
            }
        }
    }
}