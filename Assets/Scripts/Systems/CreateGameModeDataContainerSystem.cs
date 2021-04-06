using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class CreateGameModeDataContainerSystem : IEcsInitSystem 
    {
        private readonly EcsWorld _world;
        
        public void Init() 
        {
            var entity = _world.NewEntity();
            entity.Get<GameModeData>();
        }
    }
}