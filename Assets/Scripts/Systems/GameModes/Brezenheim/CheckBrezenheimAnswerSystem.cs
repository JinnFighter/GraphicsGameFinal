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
        private readonly BrezenheimDataModel _brezenheimDataModel = null;

        void IEcsRunSystem.Run() 
        {
            var lineDatas = _brezenheimDataModel.LinePoints;

            foreach(var pixelIndex in _pixelsClickedFilter)
            {
                var eventReceiver = _pixelsClickedFilter.GetEntity(pixelIndex);
                var positionComponent = _pixelsClickedFilter.Get1(pixelIndex);
                var position = positionComponent.position;
                if(_brezenheimDataModel.CurrentLine >= lineDatas.Count)
                {
                    eventReceiver.Get<GameOverEvent>();
                    return;
                }

                var lineData = lineDatas[_brezenheimDataModel.CurrentLine];
                if (position == lineData[_brezenheimDataModel.CurrentPoint])
                {
                    if(_brezenheimDataModel.CurrentPoint == lineData.Count - 1)
                    {
                        _brezenheimDataModel.CurrentLine++;
                        _brezenheimDataModel.CurrentPoint = 0;
                        if (_brezenheimDataModel.CurrentLine >= lineDatas.Count)
                        {
                            eventReceiver.Get<GameOverEvent>();
                            return;
                        }

                        eventReceiver.Get<ClearGridEvent>();
                        ref var drawData = ref eventReceiver.Get<LineDrawData>();
                        drawData.drawData = new List<(Vector2Int, Sprite)>
                        {
                            (lineDatas[_brezenheimDataModel.CurrentLine][0], _pixelSpritesContent.LineBeginningSprite),
                            (lineDatas[_brezenheimDataModel.CurrentLine][lineDatas[_brezenheimDataModel.CurrentLine].Count - 1], _pixelSpritesContent.LineEndSprite)
                        };
                    }
                    else
                    {
                        ref var drawData = ref eventReceiver.Get<LineDrawData>();
                        drawData.drawData = new List<(Vector2Int, Sprite)>
                        {
                            (position, _pixelSpritesContent.FilledSprite)
                        };
                        _brezenheimDataModel.CurrentPoint++;
                    }

                    eventReceiver.Get<CorrectAnswerEvent>();
                    _brezenheimIndexModel.Index = _brezenheimDataModel.Indexes[_brezenheimDataModel.CurrentLine][_brezenheimDataModel.CurrentPoint];
                }

                else
                {
                    eventReceiver.Get<WrongAnswerEvent>();
                }
            }
        }
    }
}