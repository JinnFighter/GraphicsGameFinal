using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Pixelgrid {

    public sealed class CreateColorContainerSystem : IEcsInitSystem 
    {
        private readonly EcsFilter<GameModeData> _filter;
        
        public void Init() 
        {
            foreach(var index in _filter)
            {
                var entity = _filter.GetEntity(index);
                ref var colorContainer = ref entity.Get<ColorContainer>();
                colorContainer.Colors = new List<Color32> { Color.black, Color.blue, Color.cyan, Color.green, Color.white, Color.yellow };
            }
        }
    }
}