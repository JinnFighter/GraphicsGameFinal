using Leopotam.Ecs;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Pixelgrid 
{
    public sealed class CheckColorPickerAnswerSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<ColorChosenEvent> _eventFilter = null;
        private readonly EcsFilter<ColorPickerData> _dataFilter = null;

        private readonly ImageHolderContainer _imageHolderContainer = null;
        private readonly Slider _slider = null;

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
                    foreach(var dataIndex in _dataFilter)
                    {
                        ref var data = ref _dataFilter.Get1(dataIndex);

                        data.CurrentColor++;
                        if(data.CurrentColor >= data.ColorCount)
                            entity.Get<GameOverEvent>();
                        else
                        {
                            var nextColor = data.Colors[data.CurrentColor];
                            var answerColor = new Color32(nextColor.r, nextColor.g, Convert.ToByte(_slider.value), 255);
                            _imageHolderContainer.QuestionHolder.color = nextColor;
                            _imageHolderContainer.AnswerHolder.color = answerColor;
                        }    
                    }
                }
                else
                    entity.Get<WrongAnswerEvent>();
            }
        }
    }
}