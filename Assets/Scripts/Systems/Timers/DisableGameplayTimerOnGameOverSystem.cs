using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class DisableGameplayTimerOnGameOverSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<GameplayTimerComponent, Counting> _timerFilter = null;
        private readonly EcsFilter<GameOverEvent> _gameOverEventFilter = null;

        void IEcsRunSystem.Run() 
        {
            if(!_gameOverEventFilter.IsEmpty())
            {
                foreach(var index in _timerFilter)
                {
                    var entity = _timerFilter.GetEntity(index);
                    entity.Del<Counting>();
                }
            }
        }
    }
}