using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class DrawFirstLineSystem : IEcsRunSystem
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
                ref var drawData = ref entity.Get<LineDrawData>();
                drawData.drawData = new List<(Vector2Int, Sprite)>
                {
                    (data.LinePoints[data.CurrentLine][0], _spritesContainer.LineBeginningSprite),
                    (data.LinePoints[data.CurrentLine][data.LinePoints[data.CurrentLine].Count - 1], _spritesContainer.LineEndSprite)
                };
            }
        }
    }
}