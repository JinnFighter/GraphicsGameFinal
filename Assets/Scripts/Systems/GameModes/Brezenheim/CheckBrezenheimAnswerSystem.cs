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

        private readonly PixelSpritesContent _pixelSpritesContent = null;
        private readonly BrezenheimIndexModel _brezenheimIndexModel = null;
        private readonly LineDataModel _lineDataModel = null;

        void IEcsRunSystem.Run() 
        {
            var lineDatas = _lineDataModel.LinePoints;

            foreach(var pixelIndex in _pixelsClickedFilter)
            {
                var eventReceiver = _pixelsClickedFilter.GetEntity(pixelIndex);
                var positionComponent = _pixelsClickedFilter.Get1(pixelIndex);
                var position = positionComponent.position;
                if(_lineDataModel.CurrentLine >= lineDatas.Count)
                {
                    eventReceiver.Get<GameOverEvent>();
                    return;
                }

                var lineData = lineDatas[_lineDataModel.CurrentLine];
                if (position == lineData[_lineDataModel.CurrentPoint])
                {
                    if(_lineDataModel.CurrentPoint == lineData.Count - 1)
                    {
                        _lineDataModel.CurrentLine++;
                        _lineDataModel.CurrentPoint = 0;
                        if (_lineDataModel.CurrentLine >= lineDatas.Count)
                        {
                            eventReceiver.Get<GameOverEvent>();
                            return;
                        }

                        eventReceiver.Get<ClearGridEvent>();
                        ref var drawData = ref eventReceiver.Get<LineDrawData>();
                        drawData.drawData = new List<(Vector2Int, Sprite)>
                        {
                            (lineDatas[_lineDataModel.CurrentLine][0], _pixelSpritesContent.LineBeginningSprite),
                            (lineDatas[_lineDataModel.CurrentLine][lineDatas[_lineDataModel.CurrentLine].Count - 1], _pixelSpritesContent.LineEndSprite)
                        };
                    }
                    else
                    {
                        ref var drawData = ref eventReceiver.Get<LineDrawData>();
                        drawData.drawData = new List<(Vector2Int, Sprite)>
                        {
                            (position, _pixelSpritesContent.FilledSprite)
                        };
                        _lineDataModel.CurrentPoint++;
                    }

                    eventReceiver.Get<CorrectAnswerEvent>();
                    _brezenheimIndexModel.Index = _lineDataModel.Indexes[_lineDataModel.CurrentLine][_lineDataModel.CurrentPoint];
                }

                else
                {
                    eventReceiver.Get<WrongAnswerEvent>();
                }
            }
        }
    }
}