using Leopotam.Ecs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class CheckSouthCohenAnswerSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<PixelPosition, PixelClickedEvent> _pixelsClickedFilter = null;
        private readonly EcsFilter<LineData, SouthCohenData> _gameModeDataFilter = null;
        private readonly EcsFilter<BorderComponent> _borderFilter = null;
        private readonly EcsFilter<PixelComponent, PixelPosition> _pixelFilter = null;

        private readonly CodeReceiver _codeReceiver = null;
        private readonly SpritesContainer _spritesContainer = null;

        void IEcsRunSystem.Run() 
        {
            var border = _borderFilter.Get1(0);
            foreach(var index in _pixelsClickedFilter)
            {
                var eventReceiver = _gameModeDataFilter.GetEntity(0);
                ref var lineData = ref _gameModeDataFilter.Get1(0);
                ref var zonesData = ref _gameModeDataFilter.Get2(0);

                if (lineData.CurrentLine == lineData.LinePoints.Count)
                    eventReceiver.Get<GameOverEvent>();
                else
                {
                    var clickedPosition = _pixelsClickedFilter.Get1(index);
                    var code = _codeReceiver.GetCode(clickedPosition.position, border.LeftCorner, border.RightCorner);
                    if (zonesData.Zones[lineData.CurrentLine].Contains(code))
                    {
                        eventReceiver.Get<CorrectAnswerEvent>();
                        zonesData.Zones[lineData.CurrentLine].Remove(code);

                        var drawData = new List<(Vector2Int, Sprite)>();
                        foreach(var pixelIndex in _pixelFilter)
                        {
                            var pixelPosition = _pixelFilter.Get2(pixelIndex);
                            if(_codeReceiver.GetCode(pixelPosition.position, border.LeftCorner, border.RightCorner) == code)
                                drawData.Add((pixelPosition.position, _spritesContainer.EmptySprite));
                        }
                        if (!zonesData.Zones[lineData.CurrentLine].Any())
                        {
                            lineData.CurrentLine++;

                            if (lineData.CurrentLine == lineData.LinePoints.Count)
                                eventReceiver.Get<GameOverEvent>();
                            else
                            {
                                eventReceiver.Get<ClearGridEvent>();
                                
                                foreach(var point in lineData.LinePoints[lineData.CurrentLine])
                                    drawData.Add((point, _spritesContainer.FilledSprite));

                                ref var lineDrawData = ref eventReceiver.Get<LineDrawData>();
                                lineDrawData.drawData = drawData;
                            }
                        }
                    }
                    else
                        eventReceiver.Get<WrongAnswerEvent>();
                }
            }
        }
    }
}