using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class GameOverOnTimerEndSystem : IEcsRunSystem 
    {
        private EcsWorld _world;
        private EcsFilter<GameplayTimerComponent, TimerEndEvent> _filter;

        void IEcsRunSystem.Run() 
        {
            if(!_filter.IsEmpty())
            {
                var entity = _world.NewEntity();
                entity.Get<GameOverEvent>();
            }
        }
    }
}