using Leopotam.Ecs;

namespace Pixelgrid 
{
    public sealed class CreateSouthCohenDataSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;

        public void Init() 
        {
            var entity = _world.NewEntity();
            entity.Get<SouthCohenData>();
        }
    }
}