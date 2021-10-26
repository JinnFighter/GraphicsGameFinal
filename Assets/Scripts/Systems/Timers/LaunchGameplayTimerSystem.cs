using Leopotam.Ecs;
using Pixelgrid.Components.Time.Timer;

namespace Pixelgrid 
{
    public sealed class LaunchGameplayTimerSystem : IEcsRunSystem 
    {
        private EcsFilter<Timer, GameplayTimerComponent>.Exclude<Counting> _timersFilter;
        private EcsFilter<StartGameEvent> _startGameFilter;
        
        void IEcsRunSystem.Run() 
        {
            if(!_startGameFilter.IsEmpty())
            {
                foreach(var index in _timersFilter)
                {
                    var entity = _timersFilter.GetEntity(index);
                    entity.Get<Counting>();
                }
            }
        }
    }
}