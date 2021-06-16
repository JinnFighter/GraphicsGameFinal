using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class DrawFirstSouthCohenLineSystem : IEcsRunSystem 
    {
        private EcsFilter<RestartGameEvent> _filter;
        private EcsFilter<GameModeData, LineData> _dataFilter;
        private SpritesContainer _spritesContainer;

        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                var entity = _filter.GetEntity(0);
                var data = _dataFilter.Get2(0);
                entity.Get<ClearGridEvent>();
                ref var drawDataComponent = ref entity.Get<LineDrawData>();
                var drawingData = new List<(Vector2Int, Sprite)>();

                foreach(var point in data.LinePoints[0])
                    drawingData.Add((point, _spritesContainer.FilledSprite));

                drawDataComponent.drawData = drawingData;
            }
        }
    }
}