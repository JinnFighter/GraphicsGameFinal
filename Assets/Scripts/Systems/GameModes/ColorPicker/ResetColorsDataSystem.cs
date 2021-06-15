using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class ResetColorsDataSystem : IEcsRunSystem 
    {
        private EcsFilter<RestartGameEvent> _filter;
        private EcsFilter<ColorContainer, ColorPickerData> _dataFilter;

        private ImageHolderContainer _imageHolderContainer;

        void IEcsRunSystem.Run() 
        {
            if(!_filter.IsEmpty())
            {
                foreach(var index in _dataFilter)
                {
                    var colorContainer = _dataFilter.Get1(index);
                    ref var data = ref _dataFilter.Get2(index);
                    var generatedColors = colorContainer.Colors;
                    var colors = new List<Color32>();

                    for(var i = 0; i < data.ColorCount; i++)
                        colors.Add(generatedColors[Random.Range(0, generatedColors.Count)]);

                    data.Colors = colors; 
                    data.CurrentColor = 0;

                    _imageHolderContainer.QuestionHolder.color = data.Colors[0];
                    _imageHolderContainer.AnswerHolder.color = Color.white;
                }
            }
        }
    }
}