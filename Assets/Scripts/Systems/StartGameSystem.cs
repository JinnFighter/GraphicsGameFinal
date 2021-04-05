using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class StartGameSystem : IEcsInitSystem 
    {
        private EcsFilter<GameplayEventReceiver> _filter;
        
        public void Init() 
        {
            foreach(var index in _filter)
            {
                var entity = _filter.GetEntity(index);
                entity.Get<StartGameEvent>();
            }
        }
    }
}