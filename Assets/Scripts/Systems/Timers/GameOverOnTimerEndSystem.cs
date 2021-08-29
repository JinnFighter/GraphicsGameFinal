using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class GameOverOnTimerEndSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<GameplayTimerComponent, TimerEndEvent> _filter = null;

        void IEcsRunSystem.Run() 
        {
            foreach (var index in _filter)
            {
                var entity = _filter.GetEntity(index);
                entity.Get<GameOverEvent>();
            }
        }
    }
}