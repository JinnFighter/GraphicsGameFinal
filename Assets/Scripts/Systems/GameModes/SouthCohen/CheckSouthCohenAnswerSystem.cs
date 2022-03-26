using System.Collections.Generic;
using System.Linq;
using Configurations.Script;
using Leopotam.Ecs;
using Pixelgrid.DataModels;
using Pixelgrid.ScriptableObjects.Sprites;
using UnityEngine;

namespace Pixelgrid.Systems.GameModes.SouthCohen 
{
    public sealed class CheckSouthCohenAnswerSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<PixelPosition, PixelClickedEvent> _pixelsClickedFilter = null;
        private readonly EcsFilter<PixelComponent, PixelPosition> _pixelFilter = null;
        private readonly EcsWorld _world = null;
        private readonly LineDataModel _lineDataModel = null;
        private readonly SouthCohenDataModel _southCohenDataModel = null;

        private readonly CodeReceiver _codeReceiver = null;
        private readonly PixelSpritesContent _pixelSpritesContent = null;
        private readonly SouthCohenConfigs _southCohenConfigs = null;
        private readonly DifficultyConfiguration _difficultyConfiguration = null;

        void IEcsRunSystem.Run() 
        {
            var lineData = _lineDataModel.LinePoints;
            var config = _southCohenConfigs[_difficultyConfiguration.Difficulty];
            foreach(var index in _pixelsClickedFilter)
            {
                var eventReceiver = _world.NewEntity();

                if (_lineDataModel.CurrentLine == lineData.Count)
                    eventReceiver.Get<GameOverEvent>();
                else
                {
                    var clickedPosition = _pixelsClickedFilter.Get1(index);
                    var code = _codeReceiver.GetCode(clickedPosition.position, config.BorderLeftCorner, config.BorderRightCorner);
                    if (_southCohenDataModel.Zones[_lineDataModel.CurrentLine].Contains(code))
                    {
                        eventReceiver.Get<CorrectAnswerEvent>();
                        _southCohenDataModel.Zones[_lineDataModel.CurrentLine].Remove(code);

                        var drawData = new List<(Vector2Int, Sprite)>();
                        foreach(var pixelIndex in _pixelFilter)
                        {
                            var pixelPosition = _pixelFilter.Get2(pixelIndex);
                            if(_codeReceiver.GetCode(pixelPosition.position, config.BorderLeftCorner, config.BorderRightCorner) == code)
                                drawData.Add((pixelPosition.position, _pixelSpritesContent.EmptySprite));
                        }
                        if (!_southCohenDataModel.Zones[_lineDataModel.CurrentLine].Any())
                        {
                            _lineDataModel.CurrentLine++;

                            if (_lineDataModel.CurrentLine == lineData.Count)
                                eventReceiver.Get<GameOverEvent>();
                            else
                            {
                                eventReceiver.Get<ClearGridEvent>();
                                
                                foreach(var point in lineData[_lineDataModel.CurrentLine])
                                    drawData.Add((point, _pixelSpritesContent.FilledSprite));

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