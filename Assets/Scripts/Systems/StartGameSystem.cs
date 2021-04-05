using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class StartGameSystem : IEcsInitSystem 
    {
        // auto-injected fields.
        readonly EcsWorld _world;
        
        public void Init() 
        {
            var entity = _world.NewEntity();
            entity.Get<StartGameEvent>();
        }
    }
}