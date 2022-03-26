using System.Collections.Generic;
using Leopotam.Ecs;
using Pixelgrid.ScriptableObjects.Sprites;
using UnityEngine;

namespace Pixelgrid.Systems.GameModes.Bezier 
{
    public sealed class DrawFirstBezierLineSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<RestartGameEvent> _filter = null;
        private readonly EcsFilter<BezierLineData> _dataFilter = null;
        
        private readonly PixelSpritesContent _pixelSpritesContent = null;

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
                        Sprite sprite;
                        
                        if(i == 0)
                            sprite = _pixelSpritesContent.LineBeginningSprite;
                        else if(i == data.Points.Count - 1)
                            sprite = _pixelSpritesContent.LineEndSprite;
                        else
                            sprite =  _pixelSpritesContent.FilledSprite;
                        
                        drawList.Add((data.Points[i], sprite));
                    }
                    drawData.drawData = drawList;
                }
                
            }
        }
    }
}