using System;
using Leopotam.Ecs;
using Pixelgrid.DataModels;
using UnityEngine;
using UnityEngine.UI;

namespace Pixelgrid.Systems.GameModes.ColorPicker 
{
    public sealed class CheckColorPickerAnswerSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<ColorChosenEvent> _eventFilter = null;

        private readonly ImageHolderContainer _imageHolderContainer = null;
        private readonly Slider _slider = null;
        private readonly ColorPickerDataModel _colorPickerDataModel = null;

        void IEcsRunSystem.Run() 
        {
            var questionColor = _imageHolderContainer.QuestionHolder.color;
            foreach (var index in _eventFilter)
            {
                var entity = _eventFilter.GetEntity(index);
                var colorData = entity.Get<ColorChosenEvent>();
                if(Math.Abs(colorData.Color.b - questionColor.b) < 0.2f)
                {
                    entity.Get<CorrectAnswerEvent>();

                    _colorPickerDataModel.CurrentColor++;
                    if(_colorPickerDataModel.CurrentColor >= _colorPickerDataModel.ColorCount)
                        entity.Get<GameOverEvent>();
                    else
                    {
                        var nextColor = _colorPickerDataModel.Colors[_colorPickerDataModel.CurrentColor];
                        var answerColor = new Color32(nextColor.r, nextColor.g, Convert.ToByte(_slider.value), 255);
                        _imageHolderContainer.QuestionHolder.color = nextColor;
                        _imageHolderContainer.AnswerHolder.color = answerColor;
                    }
                }
                else
                    entity.Get<WrongAnswerEvent>();
            }
        }
    }
}