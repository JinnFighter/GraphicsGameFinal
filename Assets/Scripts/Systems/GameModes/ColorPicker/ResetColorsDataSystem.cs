using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class ResetColorsDataSystem : IEcsRunSystem 
    {
        private EcsFilter<RestartGameEvent> _filter;
        private EcsFilter<ColorPickerData, ColorContainer> _dataFilter;

        void IEcsRunSystem.Run() 
        {
            if(!_filter.IsEmpty())
            {
                foreach(var index in _dataFilter)
                {
                    ref var data = ref _dataFilter.Get1(index);
                    var colorContainer = _dataFilter.Get2(index);
                    var generatedColors = colorContainer.Colors;
                    var colors = new List<Color32>();

                    for(var i = 0; i < data.ColorCount; i++)
                        colors.Add(generatedColors[Random.Range(0, generatedColors.Count)]);

                    data.Colors = colors; 
                    data.CurrentColor = 0;
                }
            }
        }
    }
}