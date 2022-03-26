using System;
using System.Collections.Generic;
using Leopotam.Ecs;
using Pixelgrid.DataModels;
using UnityEngine;
using UnityEngine.UI;

namespace Pixelgrid.Systems.GameModes.ColorPicker 
{
    public sealed class ResetColorsDataSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<RestartGameEvent> _filter = null;
        private readonly EcsFilter<ColorContainer> _dataFilter = null;

        private readonly ImageHolderContainer _imageHolderContainer = null;
        private readonly Slider _slider = null;
        private readonly ColorPickerDataModel _colorPickerDataModel = null;

        void IEcsRunSystem.Run() 
        {
            if(!_filter.IsEmpty())
            {
                foreach(var index in _dataFilter)
                {
                    var colorContainer = _dataFilter.Get1(index);
                    var generatedColors = colorContainer.Colors;
                    var colors = new List<Color32>();

                    for(var i = 0; i < _colorPickerDataModel.ColorCount; i++)
                        colors.Add(generatedColors[UnityEngine.Random.Range(0, generatedColors.Count)]);

                    _colorPickerDataModel.Colors = colors; 
                    _colorPickerDataModel.CurrentColor = 0;

                    var defaultColor = _colorPickerDataModel.Colors[_colorPickerDataModel.CurrentColor];
                    _imageHolderContainer.QuestionHolder.color = defaultColor;
                    var answerColor = new Color32(defaultColor.r, defaultColor.g, Convert.ToByte(_slider.value), 255);
                    _imageHolderContainer.AnswerHolder.color = defaultColor;
                    _imageHolderContainer.AnswerHolder.color = answerColor;

                    var entity = _filter.GetEntity(index);
                    ref var dataGeneratedEvent = ref entity.Get<GameModeDataGeneratedEvent>();
                    dataGeneratedEvent.DataCount = _colorPickerDataModel.ColorCount;
                }
            }
        }
    }
}