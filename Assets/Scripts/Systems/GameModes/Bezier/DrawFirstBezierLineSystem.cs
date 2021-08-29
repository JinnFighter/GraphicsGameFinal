using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Pixelgrid 
{
    public sealed class DrawFirstBezierLineSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<RestartGameEvent> _filter = null;
        private readonly EcsFilter<BezierLineData> _dataFilter = null;
        
        private readonly SpritesContainer _spritesContainer = null;

        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                foreach (var index in _dataFilter)
                {
                    var entity = _dataFilter.GetEntity(index);
                    var data = _dataFilter.Get1(index);
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
                    drawData.drawData = drawList;
                }
                
            }
        }
    }
}