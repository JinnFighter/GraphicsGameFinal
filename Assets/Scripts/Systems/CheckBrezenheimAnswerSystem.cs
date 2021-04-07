using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class CheckBrezenheimAnswerSystem : IEcsRunSystem 
    {
        private EcsFilter<PixelPosition, PixelClickedEvent> _pixelsClickedFilter;
        private EcsFilter<GameModeData, LineData, Brezenheim_D_Data> _gameModeDataFilter;
        private EcsFilter<GameplayEventReceiver> _eventReceiverFilter;
        private SpritesContainer _spritesContainer;

        void IEcsRunSystem.Run() 
        {
            ref var lineDataComponent = ref _gameModeDataFilter.Get2(0);
            var dData = _gameModeDataFilter.Get3(0);
            var lineDatas = lineDataComponent.LinePoints;
            var eventReceiver = _eventReceiverFilter.GetEntity(0);

            foreach(var pixelIndex in _pixelsClickedFilter)
            {
                var positionComponent = _pixelsClickedFilter.Get1(pixelIndex);
                var position = positionComponent.position;
                if(lineDataComponent.CurrentLine >= lineDatas.Count)
                {
                    eventReceiver.Get<GameOverEvent>();
                    return;
                }

                var lineData = lineDatas[lineDataComponent.CurrentLine];
                if (position == lineData[lineDataComponent.CurrentPoint])
                {
                    if(lineDataComponent.CurrentPoint == lineData.Count - 1)
                    {
                        lineDataComponent.CurrentLine++;
                        lineDataComponent.CurrentPoint = 0;
                        if (lineDataComponent.CurrentLine >= lineDatas.Count)
                        {
                            eventReceiver.Get<GameOverEvent>();
                            return;
                        }
                        else
                        {
                            eventReceiver.Get<ClearGridEvent>();
                            ref var drawData = ref eventReceiver.Get<LineDrawData>();
                            drawData.drawData = new List<(Vector2Int, Sprite)>
                            {
                                (lineDatas[lineDataComponent.CurrentLine][0], _spritesContainer.FilledSprite),
                                (lineDatas[lineDataComponent.CurrentLine][lineDatas[lineDataComponent.CurrentLine].Count - 1], _spritesContainer.LineEndSprite)
                            };
                        }
                    }
                    else
                    {
                        ref var drawData = ref eventReceiver.Get<LineDrawData>();
                        drawData.drawData = new List<(Vector2Int, Sprite)>
                        {
                            (position, _spritesContainer.FilledSprite)
                        };
                        lineDataComponent.CurrentPoint++;
                    }

                    eventReceiver.Get<CorrectAnswerEvent>();
                    ref var updateDEvent = ref eventReceiver.Get<UpdateDIndexEvent>();
                    updateDEvent.index = dData.Indexes[lineDataComponent.CurrentLine][lineDataComponent.CurrentPoint];
                }

                else
                {
                    eventReceiver.Get<WrongAnswerEvent>();
                }
            }
        }
    }
}