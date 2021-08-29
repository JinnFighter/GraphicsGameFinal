using Leopotam.Ecs;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class CreateTurtleSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;

        public void Init()
        {
            var entity = _world.NewEntity();
            ref var turtleComponent = ref entity.Get<TurtleComponent>();
            turtleComponent.DirectionState = new RightDirectionState();
            ref var positionComponent = ref entity.Get<PixelPosition>();
            positionComponent.position = new Vector2Int(0, 0);
        }
    }
}