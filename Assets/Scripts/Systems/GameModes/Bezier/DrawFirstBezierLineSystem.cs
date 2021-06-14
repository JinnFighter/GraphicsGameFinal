using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class DrawFirstBezierLineSystem : IEcsRunSystem 
    {
        private EcsFilter<RestartGameEvent> _filter;
        private EcsFilter<GameModeData, BezierLineData> _dataFilter;
        private SpritesContainer _spritesContainer;

        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                var entity = _filter.GetEntity(0);
                var data = _dataFilter.Get2(0);
                entity.Get<ClearGridEvent>();
                ref var drawData = ref entity.Get<LineDrawData>();
                var drawList = new List<(Vector2Int, Sprite)>();
                for(var i = 0; i < data.Points.Count; i++)
                {
                    if(i == 0)
                        drawList.Add((data.Points[i], _spritesContainer.LineBeginningSprite));
                    else if(i == data.Points.Count - 1)
                        drawList.Add((data.Points[i], _spritesContainer.LineEndSprite));
                    else
                        drawList.Add((data.Points[i], _spritesContainer.FilledSprite));
                }
                drawData.drawData = new List<(Vector2Int, Sprite)>();
            }
        }
    }
}