using Leopotam.Ecs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class CheckSouthCohenAnswerSystem : IEcsRunSystem 
    {
        private EcsFilter<PixelPosition, PixelClickedEvent> _pixelsClickedFilter;
        private EcsFilter<GameModeData, LineData, SouthCohenData> _gameModeDataFilter;
        private EcsFilter<GameplayEventReceiver> _eventReceiverFilter;
        private EcsFilter<BorderComponent> _borderFilter;
        private EcsFilter<PixelComponent, PixelPosition> _pixelFilter;

        private CodeReceiver _codeReceiver;
        private SpritesContainer _spritesContainer;

        void IEcsRunSystem.Run() 
        {
            var border = _borderFilter.Get1(0);
            foreach(var index in _pixelsClickedFilter)
            {
                var eventReceiver = _eventReceiverFilter.GetEntity(0);
                ref var lineData = ref _gameModeDataFilter.Get2(0);
                ref var zonesData = ref _gameModeDataFilter.Get3(0);

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