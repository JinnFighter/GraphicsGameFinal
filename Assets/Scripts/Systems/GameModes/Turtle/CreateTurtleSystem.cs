using Leopotam.Ecs;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class CreateTurtleSystem : IEcsInitSystem 
    {
        private readonly EcsFilter<GameModeData> _filter = null;

        public void Init() 
        {
            foreach (var index in _filter)
            {
                var entity = _filter.GetEntity(index);
                ref var turtleComponent = ref entity.Get<TurtleComponent>();
                turtleComponent.DirectionState = new RightDirectionState();
                ref var positionComponent = ref entity.Get<PixelPosition>();
                positionComponent.position = new Vector2Int(0, 0);
            }
        }
    }
}