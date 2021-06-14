using Leopotam.Ecs;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class CreateTurtleSystem : IEcsInitSystem 
    {
        // auto-injected fields.
        readonly EcsWorld _world = null;
        
        public void Init() 
        {
            var entity = _world.NewEntity();
            entity.Get<TurtleComponent>();
            ref var positionComponent = ref entity.Get<PixelPosition>();
            positionComponent.position = new Vector2Int(0, 0);
        }
    }
}