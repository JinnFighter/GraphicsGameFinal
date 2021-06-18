using Leopotam.Ecs;
using System;
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
                var colors = new List<Color32>();
                for (var i = 0; i < 6; i++)
                {
                    colors.Add(new Color32
                    {
                        r = GetColorComponentValue(),
                        g = GetColorComponentValue(),
                        b = GetColorComponentValue(),
                        a = 255
                    });
                }

                colorContainer.Colors = colors;
            }
        }

        private byte GetColorComponentValue() => Convert.ToByte(UnityEngine.Random.Range(0, 255));
    }
}