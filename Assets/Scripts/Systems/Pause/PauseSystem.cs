using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class PauseSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<PauseEvent> _filter = null;

        void IEcsRunSystem.Run() 
        {
            if(!_filter.IsEmpty())
            {
                var entity = _filter.GetEntity(0);
                entity.Get<Paused>();
                
                ref var disableSystemTypeEvent = ref entity.Get<DisableSystemTypeEvent>();
                disableSystemTypeEvent.SystemsType = "Pausable";
            }
        }
    }
}