using Leopotam.Ecs;
using System.Collections.Generic;

namespace Pixelgrid 
{
    public sealed class CreateTurtlePathSystem : IEcsInitSystem 
    {
        private EcsFilter<GameModeData> _filter;

        public void Init() 
        {
            foreach(var index in _filter)
            {
                var entity = _filter.GetEntity(index);
                ref var turtlePathComponent = ref entity.Get<TurtlePath>();
                turtlePathComponent.Path = new List<char>();
            }
        }
    }
}