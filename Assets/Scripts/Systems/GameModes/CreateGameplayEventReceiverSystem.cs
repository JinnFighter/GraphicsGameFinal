using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class CreateGameplayEventReceiverSystem : IEcsInitSystem 
    {
        private readonly EcsWorld _world;
        
        public void Init() 
        {
            var entity = _world.NewEntity();
            entity.Get<GameplayEventReceiver>();
        }
    }
}