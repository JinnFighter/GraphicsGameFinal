using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class DisableStopwatchOnGameOverSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<Stopwatch, Counting> _stopwatchFilter = null;
        private readonly EcsFilter<GameOverEvent> _gameOverEventFilter = null;

        void IEcsRunSystem.Run() 
        {
            if(!_gameOverEventFilter.IsEmpty())
            {
                foreach(var index in _stopwatchFilter)
                {
                    var entity = _stopwatchFilter.GetEntity(index);
                    entity.Del<Counting>();
                }
            }
        }
    }
}