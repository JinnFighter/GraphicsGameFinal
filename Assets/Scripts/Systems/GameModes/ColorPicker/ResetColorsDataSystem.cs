using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pixelgrid 
{
    public sealed class ResetColorsDataSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<RestartGameEvent> _filter = null;
        private readonly EcsFilter<ColorContainer, ColorPickerData> _dataFilter = null;

        private readonly ImageHolderContainer _imageHolderContainer = null;
        private readonly Slider _slider = null;

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
                        colors.Add(generatedColors[UnityEngine.Random.Range(0, generatedColors.Count)]);

                    data.Colors = colors; 
                    data.CurrentColor = 0;

                    var defaultColor = data.Colors[0];
                    _imageHolderContainer.QuestionHolder.color = defaultColor;
                    var answerColor = new Color32(defaultColor.r, defaultColor.g, Convert.ToByte(_slider.value), 255);
                    _imageHolderContainer.AnswerHolder.color = defaultColor;
                    _imageHolderContainer.AnswerHolder.color = answerColor;

                    var entity = _filter.GetEntity(index);
                    ref var dataGeneratedEvent = ref entity.Get<GameModeDataGeneratedEvent>();
                    dataGeneratedEvent.DataCount = data.ColorCount;
                }
            }
        }
    }
}