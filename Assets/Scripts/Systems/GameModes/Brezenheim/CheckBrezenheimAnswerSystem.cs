using System.Collections.Generic;
using Leopotam.Ecs;
using Pixelgrid.DataModels;
using Pixelgrid.ScriptableObjects.Sprites;
using UnityEngine;

namespace Pixelgrid.Systems.GameModes.Brezenheim 
{
    public sealed class CheckBrezenheimAnswerSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<PixelPosition, PixelClickedEvent> _pixelsClickedFilter = null;
        private readonly EcsFilter<LineData, Brezenheim_D_Data> _gameModeDataFilter = null;

        private readonly PixelSpritesContent _pixelSpritesContent = null;
        private readonly BrezenheimIndexModel _brezenheimIndexModel = null;

        void IEcsRunSystem.Run() 
        {
            ref var lineDataComponent = ref _gameModeDataFilter.Get1(0);
            var dData = _gameModeDataFilter.Get2(0);
            var lineDatas = lineDataComponent.LinePoints;
            var eventReceiver = _gameModeDataFilter.GetEntity(0);

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
                                (lineDatas[lineDataComponent.CurrentLine][0], _pixelSpritesContent.LineBeginningSprite),
                                (lineDatas[lineDataComponent.CurrentLine][lineDatas[lineDataComponent.CurrentLine].Count - 1], _pixelSpritesContent.LineEndSprite)
                            };
                        }
                    }
                    else
                    {
                        ref var drawData = ref eventReceiver.Get<LineDrawData>();
                        drawData.drawData = new List<(Vector2Int, Sprite)>
                        {
                            (position, _pixelSpritesContent.FilledSprite)
                        };
                        lineDataComponent.CurrentPoint++;
                    }

                    eventReceiver.Get<CorrectAnswerEvent>();
                    _brezenheimIndexModel.Index = dData.Indexes[lineDataComponent.CurrentLine][lineDataComponent.CurrentPoint];
                }

                else
                {
                    eventReceiver.Get<WrongAnswerEvent>();
                }
            }
        }
    }
}